namespace Veles.Application.Authentication.Interfaces
{
   using System;
   using Microsoft.Net.Http.Headers;
   using Veles.Application.DTO;

   public interface ITokenStorage
   {
      void Set(Guid commandId, AuthDto token);
      AuthDto Get(Guid commandId);
   }
}