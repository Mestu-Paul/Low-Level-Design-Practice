using ParkingLot.Enums;
using ParkingLot.Interfaces;

namespace ParkingLot.Models
{
    public abstract class AVehicle(string registerNo, VehicleType type): IVehicle
    {
        public string RegisterNo {get; set; } = registerNo;
        public VehicleType Type { get; set; } = type;
        public VehicleType GetVehicleType()
        {
            return Type;
        }

        public string GetRegisterNo()
        {
            return RegisterNo;
        }
    }
}
