namespace Veles.Infrastructure.Mongo.Documents
{
   using System;
   using MongoDB.Bson.Serialization.Attributes;
   using Veles.Domain.Entities;

   [BsonIgnoreExtraElements]
   public class RefreshTokenDocument
   {
      public Guid UserId { get; set; }

      public string Token { get; set; }

      public bool Revoked => RevokedAt.HasValue;

      public DateTime? RevokedAt { get; set; }

      public DateTime CreatedAt { get; set; }

      public DateTime ExpiryTime { get; set; }

      public RefreshTokenDocument(RefreshToken refreshToken)
      {
         UserId = refreshToken.UserId;
         Token = refreshToken.Token;
         CreatedAt = refreshToken.CreatedAt;
         ExpiryTime = refreshToken.ExpiryTime;
         RevokedAt = refreshToken.RevokedAt;
      }

      public RefreshToken ToEntity() => RefreshToken.CreateRefreshToken(UserId, Token, CreatedAt, ExpiryTime, RevokedAt);
   }
}