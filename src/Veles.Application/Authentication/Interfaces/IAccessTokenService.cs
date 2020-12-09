namespace Veles.Application.Authentication.Interfaces
{
   using System.Threading.Tasks;

   public interface IAccessTokenService
   {
      Task<bool> IsCurrentActiveToken();
      Task DeactivateCurrentAsync();
      Task<bool> IsActiveAsync(string token);
      Task DeactivateAsync(string token);
   }
}