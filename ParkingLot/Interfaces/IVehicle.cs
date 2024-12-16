using ParkingLot.Enums;

namespace ParkingLot.Interfaces
{
    public interface IVehicle
    {
        VehicleType GetVehicleType();
        string GetRegisterNo();
    }
}
