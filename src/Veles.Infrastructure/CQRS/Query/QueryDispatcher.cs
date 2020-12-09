namespace Veles.Infrastructure.CQRS.Query
{
   using System.Threading.Tasks;
   using Microsoft.Extensions.DependencyInjection;
   using Veles.Infrastructure.CQRS.Query.Interfaces;

   /// <summary>
   /// The query dispatcher
   /// </summary>
   /// <seealso cref="Veles.Infrastructure.CQRS.Query.Interfaces.IQueryDispatcher" />
   public class QueryDispatcher : IQueryDispatcher
   {
      private readonly IServiceScopeFactory _serviceScopeFactory;

      /// <summary>
      /// Initializes a new instance of the <see cref="QueryDispatcher"/> class.
      /// </summary>
      /// <param name="serviceScopeFactory">The service scope factory.</param>
      public QueryDispatcher(IServiceScopeFactory serviceScopeFactory)
      {
         _serviceScopeFactory = serviceScopeFactory;
      }

      /// <summary>
      /// Sends the specified query.
      /// </summary>
      /// <typeparam name="TResponse">The type of the response.</typeparam>
      /// <param name="query">The query.</param>
      /// <returns>Response.</returns>
      public async Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
      {
         using var scope = _serviceScopeFactory.CreateScope();
         var type = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
         dynamic handler = scope.ServiceProvider.GetRequiredService(type);

         return await handler.HandleAsync((dynamic)query);
      }

      /// <summary>
      /// Sends the specified query.
      /// </summary>
      /// <typeparam name="TQuery">The type of the query.</typeparam>
      /// <typeparam name="TResponse">The type of the response.</typeparam>
      /// <param name="query">The query.</param>
      /// <returns>Response </returns>
      public async Task<TResponse> SendAsync<TQuery, TResponse>(TQuery query) where TQuery : class, IQuery<TResponse>
      {
         using var scope = _serviceScopeFactory.CreateScope();
         var handler = scope.ServiceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();

         return await handler.HandleAsync(query);
      }
   }
}