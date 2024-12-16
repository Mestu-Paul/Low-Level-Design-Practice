using ParkingLot.Enums;

namespace ParkingLot.Models
{
    public class Truck(string registerNo) : AVehicle(registerNo, VehicleType.Truck)
    {
    }
}
