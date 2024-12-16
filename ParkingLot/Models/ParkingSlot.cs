using ParkingLot.Enums;
using ParkingLot.Interfaces;

namespace ParkingLot.Models
{
    public class ParkingSlot(int spotNumber, VehicleType type)
    {
        public int SpotNumber { get; private set; } = spotNumber;
        public VehicleType Type { get; private set; } = type;

        public IVehicle ParkedVehicle { get; private set; }

        public bool ParkVehicle(IVehicle vehicle)
        {
            if (IsAvailable()) return false;
            ParkedVehicle = vehicle;
            return true;
        }

        public bool UnParkVehicle()
        {
            if (!IsAvailable()) return false;
            ParkedVehicle = null;
            return true;
        }

        public bool IsAvailable()
        {
            return ParkedVehicle==null;
        }
    }
}
