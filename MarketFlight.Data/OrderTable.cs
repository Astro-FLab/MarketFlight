using Dapper;
using MarketFlight.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketFlight.Data
{
    public static class OrderTable
    {
        // GET - Get all orders
        public static Task<IEnumerable<OrderModel>> GetAllOrders( IDbConnection dbConnection ) =>
            dbConnection.QueryAsync<OrderModel>( "select * from MF.vOrder" );

        // POST - Create an order (from: userId(int), flight(int), orderDate(DateTime), seatCount(int))) 
        public static async Task<int> CreateOrder( IDbConnection dbConnection, int userId, int flightId, DateTime orderDate, int seatCount )
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add( "@UserId", userId );
            parameters.Add( "@FlightId", flightId );
            parameters.Add( "@OrderDate", orderDate );
            parameters.Add( "@SeatCount", seatCount );
            parameters.Add( "@OrderId", dbType: DbType.Int32, direction: ParameterDirection.Output );
            await dbConnection.ExecuteAsync( "MF.pCreateOrder", parameters, commandType: CommandType.StoredProcedure );
            return parameters.Get<int>( "@OrderId" );
        }

        // GET - Get an order (from: orderId(int))
        public static async Task<OrderModel?> GetOrderById( IDbConnection dbConnection, int orderId ) =>
            (await dbConnection.QueryAsync<OrderModel>( "select * from MF.vOrder where OrderId = @OrderId", new { OrderId = orderId } )).SingleOrDefault();
        
        
    }
}
