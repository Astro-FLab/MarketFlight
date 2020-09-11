using DbUp;
using DbUp.Engine;
using System;
using System.Reflection;

namespace MarketFlight.Data
{
    public class Migration
    {
        public static void DbSetup( string connectionString )
        {
            UpgradeEngine upgradeEngine = DeployChanges.To
                .SqlDatabase( connectionString )
                .WithScriptsEmbeddedInAssembly( typeof( AirportTable ).Assembly )
                .LogToConsole()
                .Build();

            DatabaseUpgradeResult result = upgradeEngine.PerformUpgrade();
            if( !result.Successful )
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( result.Error );
                Console.ResetColor();
            }
        }
    }
}
