namespace Veles.Infrastructure.Mongo.Repositories
{
   using System.Threading.Tasks;
   using MongoDB.Driver;
   using MongoDB.Driver.Linq;
   using Veles.Domain.Entities;
   using Veles.Domain.Repository;
   using Veles.Infrastructure.Mongo.Documents;

   public class RefreshTokenRepository : IRefreshTokenRepository
   {
      private readonly IMongoCollection<RefreshTokenDocument> _collection;

      public RefreshTokenRepository(IMongoDatabase database)
      {
         _collection = database.GetCollection<RefreshTokenDocument>("refreshTokens");
      }

      public async Task AddAsync(RefreshToken refreshToken)
      {
         await _collection.InsertOneAsync(new RefreshTokenDocument(refreshToken));
      }

      public async Task<RefreshToken> GetAsync(string token)
      {
         var refreshToken = await _collection
            .AsQueryable()
            .SingleOrDefaultAsync(x => x.Token == token);

         return refreshToken?.ToEntity();
      }

      public async Task UpdateAsync(RefreshToken refreshToken)
      {
         var document = new RefreshTokenDocument(refreshToken);
         await _collection.ReplaceOneAsync(x => x.UserId == document.UserId, document);
      }
   }
}