using ParkingLot.Interfaces;

namespace ParkingLot.Models
{
    public class ParkingLot
    {
        public List<Level> Levels { get; set; }

        public bool ParkVehicle(IVehicle vehicle)
        {
            foreach (var level in Levels)
            {
                if (level.ParkVehicle(vehicle)) return true;
            }

            return false;
        }

        public bool UnParkVehicle(IVehicle vehicle)
        {
            foreach (var level in Levels)
            {
                if (level.UnParkVehicle(vehicle))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddLevel(Level level)
        {
            Levels.Add(level);
        }

        public void Display()
        {
            for (int i = 0; i < Levels.Count; i++)
            {
                Levels[i].Display();
            }
        }

    }
}
