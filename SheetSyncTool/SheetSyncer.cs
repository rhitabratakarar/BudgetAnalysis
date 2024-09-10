using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
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
        public async void Sync()
        {
            UserCredential? credential = await GetValidatedUserCredential();
            SheetsService? sheetsService = null;

            if (credential != null)
                sheetsService = GetSheetsService(credential);

            if (sheetsService != null)
            {
                // TODO: if got a validated sheets service, then run the operations...
                // var sheets = await sheetsService.Spreadsheets.Values.Get();

            }
        }

        /// <summary>
        /// method utilized to validate secrets json file.
        /// </summary>
        /// <returns>UserCredential which is Google Validated</returns>
        private async Task<UserCredential?> GetValidatedUserCredential()
        {
            string CLIENT_SECRETS_FILENAME = this.configuration["ClientSecretFile"]!;
            UserCredential? credential = null;

            if (CLIENT_SECRETS_FILENAME != null)
            {
                try
                {
                    using (var stream = new FileStream(CLIENT_SECRETS_FILENAME, FileMode.Open, FileAccess.Read))
                    {
                        credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                                GoogleClientSecrets.FromStream(stream).Secrets,
                                    new[] { SheetsService.Scope.Spreadsheets },
                                    "user", CancellationToken.None, new FileDataStore("Books.ListMyLibrary"));
                    }
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    Console.WriteLine(fileNotFoundException.Message + "<The passed file is absent from the directory>");
                }
            }


            return credential;
        }

        /// <summary>
        /// This will provide the sheets service which is created using a validated User credential.
        /// </summary>
        /// <param name="credential">This should be the validated credential</param>
        /// <returns>Sheets Service reference</returns>
        private SheetsService? GetSheetsService(UserCredential credential)
        {
            try
            {
                SheetsService service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = configuration["ApplicationName"] == null ? "Analyzer" : configuration["ApplicationName"]
                });
                return service;
            }
            catch (AggregateException exceptions)
            {
                foreach (Exception e in exceptions.InnerExceptions)
                {
                    Console.WriteLine("ERROR: " + e.Message);
                }
                return null;
            }
        }

    }
}
