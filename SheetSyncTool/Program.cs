using Microsoft.Extensions.Configuration;

namespace SheetSyncTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string APP_SETTINGS = "appsettings.json";
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddJsonFile(APP_SETTINGS)
                                            .Build();

            try
            {
                ISheetSyncer sheetSyncer = new SheetSyncer(configuration);
                sheetSyncer.Sync();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception has occured: " + e.Message);
                Console.WriteLine("Look into the stack Trace: " + e.StackTrace);
            }
        }
    }
}
