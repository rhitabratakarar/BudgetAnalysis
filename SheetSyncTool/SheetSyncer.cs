using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace SheetSyncTool
{
    internal class SheetSyncer : ISheetSyncer
    {
        private readonly IConfiguration configuration;

        public SheetSyncer(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// This method will sync the changes with the google sheet mentioned and will update the database.
        /// </summary>

        public async Task Sync()
        {
            await Execute();
            return;
        }

        /// <summary>
        /// Method to start execution of sheet operations.
        /// </summary>
        /// <returns></returns>
        private async Task Execute()
        {
            string CLIENT_SECRETS_FILE = this.configuration["ClientSecretFile"]!;
            string spreadSheetId = this.configuration["SheetID"]!;
            string rangeInSheet = this.configuration["SheetName"]! + "!" + this.configuration["SheetColumnRange"]!;

            UserCredential? credential = null;


            #region GENERATE VALID CREDENTIAL.
            if (CLIENT_SECRETS_FILE != null)
            {
                try
                {
                    using (var stream = new FileStream(CLIENT_SECRETS_FILE, FileMode.Open, FileAccess.Read))
                    {
                        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                                GoogleClientSecrets.FromStream(stream).Secrets,
                                new[] { SheetsService.Scope.Spreadsheets },
                                Environment.UserName, CancellationToken.None,
                                new FileDataStore("BudgetAnalysis.SheetSyncer"));
                    }
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    throw new FileNotFoundException(fileNotFoundException.Message);
                }
                catch (Exception e)
                {
                    throw new Exception("❌ ERROR: " + e.Message);
                }
            }
            #endregion


            #region CREATE SPREADSHEET'S SERVICE.
            SheetsService service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.configuration["ApplicationName"]!
            });
            #endregion


            #region EXECUTE SPREADSHEET OPERATIONS.

            IList<IList<object>> contentToSend = new List<IList<object>>();

            if (service != null && spreadSheetId != null && spreadSheetId != "")
            {
                try
                {
                    SpreadsheetsResource.ValuesResource.GetRequest sheetsRequest = service.Spreadsheets.Values.Get(spreadSheetId, rangeInSheet);
                    ValueRange values = await sheetsRequest.ExecuteAsync();

                    contentToSend = values.Values;
                }
                catch (AggregateException exs)
                {
                    foreach (Exception e in exs.InnerExceptions)
                    {
                        Console.WriteLine("❌ ERROR: " + e.Message);
                    }
                    return;
                }
            }
            #endregion


            #region FORMAT CONTENT BEFORE SENDING

            IDictionary<string, IList<IList<object>>> formattedContentToSend = new Dictionary<string, IList<IList<object>>>();

            if (contentToSend != null)
            {
                contentToSend.Insert(0, new List<object>() { "Year", this.configuration["SheetYear"]! });
                contentToSend.Insert(1, new List<object>() { "Month", this.configuration["SheetName"]! });
                contentToSend.Insert(2, new List<object>() { "SheetColumnRange", this.configuration["SheetColumnRange"]! });

                formattedContentToSend.Add("InsertionData", contentToSend);
            }
            else
            {
                throw new Exception("❌ Received Empty content from GCloud.");
            }

            #endregion


            #region INSERT INTO DATABASE.

            if (formattedContentToSend != null)
            {
                HttpClient client = new HttpClient();

                StringContent stringContent = new StringContent(JsonSerializer.Serialize(formattedContentToSend),
                                                                    encoding: Encoding.UTF8,
                                                                    "application/json");

                HttpResponseMessage response = await client.PostAsync(this.configuration["DataInsertionDbAPI"], stringContent);

                if (response.IsSuccessStatusCode)
                    Console.WriteLine("Received 200 status code! OK. 👍");
                else
                    throw new Exception("❌ Success status not received. Debug to find out.");
            }
            else
            {
                throw new Exception("Content is missing 😵‍💫. Debug to find it out 😓");
            }
            #endregion

            return;
        }
    }
}
