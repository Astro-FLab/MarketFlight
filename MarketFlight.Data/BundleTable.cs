using Dapper;
using MarketFlight.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlight.Data
{
    public static class BundleTable
    {
        public static Task<int> CreateBundle( IDbConnection dbConnection, double money )
         => dbConnection.QuerySingleAsync<int>( "insert into MF.tBundle (Price) output inserted.BundleId values(@Price)", new { Price = money } );

        public static Task AddBundleItem( IDbConnection dbConnection, int bundleId, int flightId )
            => dbConnection.ExecuteAsync( "insert into MF.tBundleItems (BundleId, FlightId) values(@BundleId, @FlightId)", new { BundleId = bundleId, FlightId = flightId } );

        public static async Task<BundleModel> GetBundleById( IDbConnection db, int bundleId )
        {
            IDataReader? reader = await db.ExecuteReaderAsync( "select * from MF.tBundle where BundleId = @BundleId;" +
                "select f.FlightId, f.DepartureAirportId, f.DepartureAirportName, f.ArrivalAirportId, ArrivalAirportName from MF.tBundleItems bi join MF.vFlight f on bi.FlightId = f.FlightId where bi.BundleId = @BundleId;", new { BundleId = bundleId } );
            var bundle = reader.Parse<BundleModel>().Single();
            reader.NextResult();
            var flights = reader.Parse<FlightModel>().ToList();
            bundle.Flights = flights;
            return bundle;
        }
    }
}
