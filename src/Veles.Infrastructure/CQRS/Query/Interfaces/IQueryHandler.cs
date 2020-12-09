namespace Veles.Infrastructure.CQRS.Query.Interfaces
{
   using System.Threading.Tasks;

   public interface IQueryHandler<in TQuery, TResponse> where TQuery : class, IQuery
   {
      Task<TResponse> HandleAsync(TQuery request);
   }
}