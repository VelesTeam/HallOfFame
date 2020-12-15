namespace Veles.Tests.Repositories
{
   using System;
   using System.Threading.Tasks;
   using FluentAssertions;
   using Veles.Domain.Entities;
   using Xunit;

   public class RefreshTokenRepositoryTests : IClassFixture<RefreshTokenRepositoryFixture>
   {
      private RefreshTokenRepositoryFixture _context;
      
      public RefreshTokenRepositoryTests(RefreshTokenRepositoryFixture context)
      {
         _context = context;
      }

      [Fact]
      public async Task WhenCalledAddMethod_ShouldSuccessfullyAddRefreshTokenToDatabase()
      {
         // arrange
         var refreshToken =
            RefreshToken.CreateRefreshToken(Guid.NewGuid(), "testToken", DateTime.UtcNow, DateTime.UtcNow.AddDays(5));

         // act
         await _context.RefreshTokenRepository.AddAsync(refreshToken);

         // assert
         var result = await _context.RefreshTokenRepository.GetAsync(refreshToken.Token);
         result.CreatedAt.ToShortTimeString().Should().Be(refreshToken.CreatedAt.ToShortTimeString());
         result.ExpiryTime.ToShortTimeString().Should().Be(refreshToken.ExpiryTime.ToShortTimeString());
         result.Token.Should().Be(refreshToken.Token);
         result.UserId.Should().Be(refreshToken.UserId);
         result.Revoked.Should().BeFalse();
         result.RevokedAt.Should().BeNull();
      }
   }
}