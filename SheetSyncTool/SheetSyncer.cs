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

            UserCredential credential = await GenerateAndGetValidCredential(CLIENT_SECRETS_FILE);

            SheetsService service = GetSpreadSheetService(credential);

            IList<IList<object>> contentToSend = await GetContentToSendFromSpreadSheet(spreadSheetId, rangeInSheet, service);

            IDictionary<string, IList<IList<object>>> marshalledContent = GetMarshalledContent(contentToSend);

            await SaveToDatabase(marshalledContent);
        }

        private async Task<UserCredential> GenerateAndGetValidCredential(string clientSecretFile)
        {
            UserCredential? credential = null;

            if (clientSecretFile != null)
            {
                try
                {
                    using (var stream = new FileStream(clientSecretFile, FileMode.Open, FileAccess.Read))
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

            if (credential != null)
                return credential;
            else
                throw new Exception("Find NULL for credential object.");
        }

        private async Task SaveToDatabase(IDictionary<string, IList<IList<object>>>? marshalledContent)
        {
            if (marshalledContent != null)
            {
                HttpClient client = new HttpClient();

                StringContent stringContent = new StringContent(JsonSerializer.Serialize(marshalledContent),
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
        }

        private SheetsService GetSpreadSheetService(UserCredential credential)
        {
            SheetsService service;
            try
            {
                service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = this.configuration["ApplicationName"]!
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return service;
        }

        private async Task<IList<IList<object>>> GetContentToSendFromSpreadSheet(string spreadSheetId, string rangeInSheet, SheetsService service)
        {
            IList<IList<object>>? contentToSend = null;

            if (spreadSheetId != "")
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
                    throw;
                }
            }

            if (contentToSend != null)
                return contentToSend;
            else
                throw new Exception("Received NULL while getting content.");
        }

        public IDictionary<string, IList<IList<object>>> GetMarshalledContent(IList<IList<object>> content)
        {
            IDictionary<string, IList<IList<object>>> marshalledContent = new Dictionary<string, IList<IList<object>>>();

            if (content != null)
            {
                content.Insert(0, new List<object>() { "Year", this.configuration["SheetYear"]! });
                content.Insert(1, new List<object>() { "Month", this.configuration["SheetName"]! });
                content.Insert(2, new List<object>() { "SheetColumnRange", this.configuration["SheetColumnRange"]! });

                marshalledContent.Add("InsertionData", content);
            }
            else
            {
                throw new Exception("❌ Received Empty content from GCloud.");
            }

            return marshalledContent;
        }
    }
}
