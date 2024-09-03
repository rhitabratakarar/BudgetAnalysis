using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SheetSyncTool
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddUserSecrets(Assembly.GetExecutingAssembly())
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
