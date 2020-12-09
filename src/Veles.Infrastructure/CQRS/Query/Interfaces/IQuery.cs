namespace Veles.Infrastructure.CQRS.Query.Interfaces
{
   /// <summary>
   /// IQuery
   /// </summary>
   public interface IQuery
   {
   }

   /// <summary>
   /// IQuery
   /// </summary>
   /// <typeparam name="TResponse">The type of the response.</typeparam>
   public interface IQuery<TResponse> : IQuery
   {
   }
}