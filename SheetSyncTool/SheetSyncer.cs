using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Configuration;

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
            string rangeInSheet = this.configuration["SheetColumnRange"]!;

            UserCredential? credential = null;

            #region Generating a valid credential.
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
                    Console.WriteLine(fileNotFoundException.Message + "<The passed file is absent from the directory>");
                }
                catch (Exception e)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
            }
            #endregion


            #region create a sheet service to connect to existing sheet in cloud.
            SheetsService service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.configuration["ApplicationName"]!
            });
            #endregion


            #region execute spreadsheet operations...
            if (service != null && spreadSheetId != null && spreadSheetId != "")
            {
                try
                {
                    SpreadsheetsResource.ValuesResource.GetRequest sheetsRequest = service.Spreadsheets.Values.Get(spreadSheetId, rangeInSheet);
                    ValueRange values = await sheetsRequest.ExecuteAsync();
                    Console.WriteLine(values);
                }
                catch (AggregateException exs)
                {
                    foreach (Exception e in exs.InnerExceptions)
                    {
                        Console.WriteLine("ERROR: " + e.Message);
                    }
                }
            }
            #endregion

            return;
        }
    }
}
