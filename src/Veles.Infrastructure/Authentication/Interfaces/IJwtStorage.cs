namespace Veles.Infrastructure.Authentication.Interfaces
{
   using System;
   using Veles.Application.DTO;

   public interface IJwtStorage
   {
      void Set(Guid commandId, AuthDto token);
      AuthDto Get(Guid commandId);
   }
}