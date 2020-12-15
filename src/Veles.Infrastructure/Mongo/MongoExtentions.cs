namespace Veles.Infrastructure.Mongo
{
   using Microsoft.Extensions.Configuration;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.Options;
   using MongoDB.Driver;
   using Veles.Domain.Repository;
   using Veles.Infrastructure.Mongo.Repositories;

   public static class MongoExtentions
   {
      public static void AddMongoDB(this IServiceCollection services, IConfiguration config)
      {
         services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

         services.Configure<MongoOptions>(config.GetSection("mongo"));
         services.AddSingleton<IMongoClient>(x =>
         {
            var options = x.GetService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
         });

         services.AddTransient(x =>
         {
            var options = x.GetService<IOptions<MongoOptions>>().Value;
            var client = x.GetService<IMongoClient>();

            return client.GetDatabase(options.Database);
         });
      }
   }
}