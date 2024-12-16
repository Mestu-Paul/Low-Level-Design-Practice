namespace ParkingLot.Interfaces
{
    public interface ICommandHandler<in TCommand, TResponse> where TCommand:ICommand
    {
        Task<TResponse> HandleAsync(TCommand command);
    }
}
