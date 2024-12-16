using ParkingLot.Commands;
using ParkingLot.Interfaces;

namespace ParkingLot.CommandHandlers
{
    public class CreateParkingLotCommandHandler : ICommandHandler<CreateParkingLotCommand,string>
    {
        public async Task<string> HandleAsync(CreateParkingLotCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
