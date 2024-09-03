using Microsoft.Extensions.Configuration;

namespace SheetSyncTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddJsonFile("appsettings.json")
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
