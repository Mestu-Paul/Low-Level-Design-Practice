using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Commands
{
    public class CreateParkingLotCommand : ICommand
    {
        public CreateParkingLotCommand(ParkingLotDto parkingLotDto)
        {
            ParkingLotId = parkingLotDto.ParkingLotId;
            NumberOfFloors = parkingLotDto.NumberOfFloors;
            NumberOfSlotsPerFloor = parkingLotDto.SlotsPerFloor;
        }
        public CreateParkingLotCommand(string command)
        {
            var tokens = command.Split(' ').ToList();
            if (tokens.Count != 4 || tokens[0] != "create_parking_lot")
            {
                throw new Exception("Invalid command or insufficient parameters to create a new parking lot.");
            }

            ParkingLotId = tokens[1].Trim();
            NumberOfFloors = ParseInto(tokens[2], "Number of floors");
            NumberOfSlotsPerFloor = ParseInto(tokens[3], "Number of slots per floor");
        }

        private int ParseInto(string token, string param)
        {
            return int.TryParse(token, out int intValue)
                ? intValue
                : throw new Exception($"{param} {token} is not valid");
        }

        public string ParkingLotId { get; private set; } = string.Empty;
        public int NumberOfFloors { get; private set; }
        public int NumberOfSlotsPerFloor { get; private set; }
    }
}
