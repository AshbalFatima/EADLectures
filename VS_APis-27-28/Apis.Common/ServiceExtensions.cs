using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Apis.Common.Settings;

namespace Apis.Common
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            services.AddSingleton(services => {

                var configService = services.GetService<IConfiguration>();
                var mongosetting = configService.GetSection(nameof(MongoSettings)).Get<MongoSettings>();

                var dbsetting = configService.GetSection(nameof(MongoDb)).Get<MongoDb>();

                //var mongoClient = new MongoClient("mongodb:localhost:27017");
                var mongoClient = new MongoClient(mongosetting.ConnectionString);
                var _database = mongoClient.GetDatabase(dbsetting.database);

                return _database;

            });
            return services;
        }
        //services.AddRepository<Product>("xyz");


        //

        public static IServiceCollection AddRepository<T>(this IServiceCollection services,string collectionName) where T : IEntity
        {
            //services.AddRepository<Customer>("customers")
            services.AddSingleton<IRepository<T>>(services =>
            {

                var dbService = services.GetService<IMongoDatabase>();
                return new Repository<T>(dbService, collectionName);

            });
            return services;

        
        }
    }


    public static class forMueez
    { 
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value); 
        }
    }

}
