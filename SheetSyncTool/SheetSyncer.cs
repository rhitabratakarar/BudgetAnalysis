using Microsoft.Extensions.Configuration;

namespace SheetSyncTool
{
    internal class SheetSyncer : ISheetSyncer
    {
        private readonly IConfiguration configuration;
        private readonly string? API_KEY;

        public SheetSyncer(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.API_KEY = this.configuration["SHEETS_ONLY_API_KEY"];

            if (this.API_KEY == null)
                throw new Exception("api key is missing in user secrets.");
        }

        public void Sync()
        {
            throw new NotImplementedException();
        }
    }
}
