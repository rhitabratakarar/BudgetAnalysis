using DataAccess;
using DataAccess.Models;
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

        private readonly IDbDataAccess dbDataAccess;

        public SheetSyncer(IConfiguration configuration, IDbDataAccess dbDataAccess)
        {
            this.configuration = configuration;
            this.dbDataAccess = dbDataAccess;
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

            IList<IList<object>> contentToSaveInDatabase = await GetContentToSaveIntoDatabase(spreadSheetId, rangeInSheet, service);

            await SaveToDatabase(contentToSaveInDatabase);
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

        private async Task SaveToDatabase(IList<IList<object>> contentToSave)
        {
            try
            {
                throw new NotImplementedException("SaveToDatabase Not implemented yet.");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
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

        private async Task<IList<IList<object>>> GetContentToSaveIntoDatabase(string spreadSheetId, string rangeInSheet, SheetsService service)
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
    }
}
