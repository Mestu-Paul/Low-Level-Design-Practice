using ParkingLot.Enums;

namespace ParkingLot.Models
{
    public class Car(string registerNo) : AVehicle(registerNo, VehicleType.Car)
    {
    }
}
