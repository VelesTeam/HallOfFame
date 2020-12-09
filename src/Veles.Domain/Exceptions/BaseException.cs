namespace Veles.Domain.Exceptions
{
   using System;

   public abstract class BaseException : Exception
   {
      protected string Code { get; }

      protected BaseException(string code, string message) : base(message)
      {
         Code = code;
      }

   }
}