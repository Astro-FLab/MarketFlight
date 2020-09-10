using System;
using System.Collections.Generic;
using System.Text;

namespace MarketFlight.Data
{
    class Migration
    {
        var connectionString = args.FirstOrDefault() ?? "Server=(local)\\SqlExpress; Database=MyApp; Trusted_connection=true";
        public static Configuration()
        {
            var upgradeEngine = DeployChanges.To
                .SqlDatabase( connectionString )
                .WithScriptsEmbeddedInAssembly( Assembly.GetExecutingAssembly() )
                .LogToConsole()
                .Build();
            var result = upgrader.PerformUpgrade();

            if( !result.Successful )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( result.Error );
                Console.ResetColor();
#if DEBUG
                    Console.ReadLine();
#endif
                return -1;
            }
        }
    }
}
