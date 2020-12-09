namespace Veles.Application.DTO
{
   using System;

   public class AuthDto
   {
      public Guid UserId { get; set; }
      public string AccessToken { get; set; }
      public string RefreshToken { get; set; }
      public long Expires { get; set; }
   }
}