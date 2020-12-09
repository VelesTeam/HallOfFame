namespace Veles.Infrastructure.CQRS.Command
{
   using System.Threading.Tasks;
   using Microsoft.Extensions.DependencyInjection;
   using Veles.Infrastructure.CQRS.Command.Interfaces;

   /// <summary>
   /// The command dispatcher
   /// </summary>
   /// <seealso cref="Veles.Infrastructure.CQRS.Command.Interfaces.ICommandDispatcher" />
   public class CommandDispatcher : ICommandDispatcher
   {
      private readonly IServiceScopeFactory _serviceScopeFactory;

      /// <summary>
      /// Initializes a new instance of the <see cref="CommandDispatcher"/> class.
      /// </summary>
      /// <param name="serviceScopeFactory">The service scope factory.</param>
      public CommandDispatcher(IServiceScopeFactory serviceScopeFactory)
      {
         _serviceScopeFactory = serviceScopeFactory;
      }

      /// <summary>
      /// Sends the specified command.
      /// </summary>
      /// <typeparam name="TCommand">The type of the command.</typeparam>
      /// <param name="command">The command.</param>
      public async Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand
      {
         using var scope = _serviceScopeFactory.CreateScope();
         var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();
         await handler.HandleAsync(command);
      }
   }
}