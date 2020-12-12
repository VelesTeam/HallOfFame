namespace Veles.Infrastructure.Authentication
{
   public class JsonWebToken
   {
      public string Id { get; set; }
      public string AccessToken { get; set; }
      public long Expires { get; set; }
      public string RefreshToken { get; set; }
   }
}