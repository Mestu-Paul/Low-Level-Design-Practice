using ParkingLot.Enums;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Services
{
    public class ParkingLotService : IParkingLotService
    {
        private List<Models.ParkingLot> parkingLots { get; set; }
        public ParkingLotService()
        {
            parkingLots = new List<Models.ParkingLot>
            {
                new Models.ParkingLot()
            };
        }

        public void AddNewLevel(int slots, int parkingLotNumber = 0)
        {
            if (slots <= 3) throw new Exception("Very few number of slots, can not add new level");

            var slotDistributions = new Dictionary<VehicleType, int>
            {
                { VehicleType.Truck, 1 },
                { VehicleType.MotorCycle, 2 },
                { VehicleType.Car, slots - 3 }
            };
            parkingLots[parkingLotNumber].AddLevel(new Level(slotDistributions));
        }

        public bool ParkVehicle(IVehicle vehicle)
        {
            foreach (var parkingLot in parkingLots)
            {
                if (parkingLot.ParkVehicle(vehicle)) return true;
            }

            return false;
        }

        public bool UnParkVehicle(IVehicle vehicle)
        {
            foreach (var parkingLot in parkingLots)
            {
                if (parkingLot.UnParkVehicle(vehicle)) return true;
            }

            return false;
        }

        public void Display()
        {
            for (int i = 0; i < parkingLots.Count; i++)
            {
                Logger.Info($"For {i}th parking lot : \n");
                parkingLots[i].Display();
            }
        }
    }
}
