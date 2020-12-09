namespace Veles.Infrastructure.CQRS.Command.Interfaces
{
   using System.Threading.Tasks;

   /// <summary>
   /// ICommandHandler
   /// </summary>
   /// <typeparam name="TCommand">The type of the command.</typeparam>
   public interface ICommandHandler<in TCommand>
      where TCommand : class, ICommand
   {
      /// <summary>
      /// Handles the specified command.
      /// </summary>
      /// <param name="command">The command.</param>
      /// <returns></returns>
      Task HandleAsync(TCommand command);
   }
}