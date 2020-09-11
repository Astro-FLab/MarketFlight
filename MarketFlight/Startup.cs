using System;
using System.Data;
using System.Data.SqlClient;
using MarketFlight.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarketFlight
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        string _dbConnectionString;
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddControllers();
            // Inject IDbConnection, with implementation from SqlConnection class.
            services.AddTransient<IDbConnection>( ( sp ) => new SqlConnection( _dbConnectionString ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            // _dbConnectionString = Configuration.GetConnectionString( "dbConnection" );
            // if( env.IsProduction() )
            // {
                _dbConnectionString = "Data Source=127.0.0.1,1433;Network Library=DBMSSOCN;Initial Catalog=MarketFlight;User ID=sa;Password=MarketFlight10;";// Environment.GetEnvironmentVariable( "dbConnection" )!;
            // }
            
            Console.WriteLine( "connection string:" + _dbConnectionString );
            Migration.DbSetup( _dbConnectionString );
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseCors( a =>
                a.WithOrigins( "http://localhost:5000" )
                .WithOrigins( "localhost:5000" )
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
             );
            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllers();
             } );
        }
    }
}
