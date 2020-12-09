namespace Veles.Domain.Repository
{
   using System.Threading.Tasks;
   using Veles.Domain.Entities;

   public interface IRefreshTokenRepository
   {
      Task AddAsync(RefreshToken refreshToken);
      Task<RefreshToken> GetAsync(string token);
      Task UpdateAsync(RefreshToken refreshToken);
   }
}
