namespace Veles.Infrastructure.CQRS.Command.Interfaces
{
   using System.Threading.Tasks;

   /// <summary>
   /// ICommandDispatcher
   /// </summary>
   public interface ICommandDispatcher
   {
      /// <summary>
      /// Sends the specified command.
      /// </summary>
      /// <typeparam name="TCommand">The type of the command.</typeparam>
      /// <param name="command">The command.</param>
      /// <returns>Task</returns>
      Task SendAsync<TCommand>(TCommand command) where TCommand : class, ICommand;

   }
}