using ParkingLot.Enums;
using ParkingLot.Interfaces;

namespace ParkingLot.Models
{
    public class Level
    {
        public Dictionary<VehicleType,int> SlotDistribution {get; private set; }

        public Level(Dictionary<VehicleType, int> slotDistribution)
        {
            SlotDistribution = slotDistribution;
            foreach (var i in SlotDistribution)
            {
                var parkingSlots = new List<ParkingSlot>();
                for (var k = 0; k < i.Value; k++)
                {
                    parkingSlots.Add(new ParkingSlot(k+1,i.Key));
                }
                _slots.Add(i.Key,parkingSlots);
            }
        }

        //TODO: implement it with priority queue
        private Dictionary<VehicleType,List<ParkingSlot>> _slots { get; set; }

        public bool ParkVehicle(IVehicle vehicle)
        {
            foreach (var parkingSlot in _slots[vehicle.GetVehicleType()])
            {
                if (parkingSlot.ParkVehicle(vehicle))
                {
                    return true;
                }
            }
            return false;

        }

        public bool UnParkVehicle(IVehicle vehicle)
        {
            foreach (var parkingSlot in _slots[vehicle.GetVehicleType()])
            {
                if (parkingSlot.ParkedVehicle.GetRegisterNo() == vehicle.GetRegisterNo())
                {
                    return parkingSlot.UnParkVehicle();
                }
            }

            return false;
        }

        public void Display()
        {
            foreach (var slots in _slots)
            {
                foreach (var slot in slots.Value)
                {
                    slot.Display();
                }
            }
        }


    }
}
