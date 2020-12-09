namespace Veles.Infrastructure.CQRS.Query.Interfaces
{
   using System.Threading.Tasks;

   /// <summary>
   /// IQueryDispatcher
   /// </summary>
   public interface IQueryDispatcher
   {
      /// <summary>
      /// Sends the specified query.
      /// </summary>
      /// <typeparam name="TResponse">The type of the response.</typeparam>
      /// <param name="query">The query.</param>
      /// <returns>TResponse</returns>
      Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);


      /// <summary>
      /// Sends the specified query.
      /// </summary>
      /// <typeparam name="TQuery">The type of the query.</typeparam>
      /// <typeparam name="TResponse">The type of the response.</typeparam>
      /// <param name="query">The query.</param>
      /// <returns>TResponse</returns>
      Task<TResponse> SendAsync<TQuery, TResponse>(TQuery query) where TQuery : class, IQuery<TResponse>;
   }
}