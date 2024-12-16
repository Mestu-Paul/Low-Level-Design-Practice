using ParkingLot.Enums;

namespace ParkingLot.Models
{
    public class MotorCycle(string registerNo) :AVehicle(registerNo, VehicleType.MotorCycle)
    {
    }
}
