﻿namespace Veles.Infrastructure.Middleware
{
   public class ErrorResponse
   {
      public string Code { get; set; }

      public string Message { get; set; }

      public object Details { get; set; }
   }
}