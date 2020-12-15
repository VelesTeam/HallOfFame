namespace Veles.Tests.Repositories
{
   using System;
   using MongoDB.Driver;
   using Veles.Infrastructure.Mongo;
   using Veles.Infrastructure.Mongo.Repositories;

   public class RefreshTokenRepositoryFixture : IDisposable
   {
      private IMongoClient mongoClient;

      public RefreshTokenRepositoryFixture()
      {
         var options = new MongoOptions
         {
            ConnectionString = "mongodb://localhost:27017",
            Database = "Test"
         };
         mongoClient = new MongoClient(options.ConnectionString);

         RefreshTokenRepository = new RefreshTokenRepository(mongoClient.GetDatabase(options.Database));
      }

      public RefreshTokenRepository RefreshTokenRepository { get; }
      
      public void Dispose()
      {
         mongoClient.DropDatabase("Test");
      }
   }
}