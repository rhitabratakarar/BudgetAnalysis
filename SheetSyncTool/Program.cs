using DataAccess;
using Microsoft.Extensions.Configuration;

namespace SheetSyncTool
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            const string APP_SETTINGS = "appsettings.json";
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddJsonFile(APP_SETTINGS)
                                            .Build();

            IDbDataAccess? dbDataAccess = null;
            string? dbConnectionString = configuration.GetConnectionString("BudgetAnalysis");

            if (dbConnectionString != null)
                dbDataAccess = new DbDataAccess(dbConnectionString);

            try
            {
                if (dbDataAccess == null)
                    throw new NullReferenceException("Not able to initialize data access");

                ISheetSyncer sheetSyncer = new SheetSyncer(configuration, dbDataAccess);
                sheetSyncer.Sync().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occured: " + e.Message);
                Console.WriteLine("Look into the stack Trace: " + e.StackTrace);
            }
        }
    }
}
