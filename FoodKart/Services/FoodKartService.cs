using System.Reflection;
using Core.Attributes;
using Core.Utility;
using FoodKart.Interfaces;
using FoodKart.Models;

namespace FoodKart.Services
{
    public class FoodKartService
    {
        private IRestaurantService _restaurantService = RestaurantService.GetInstance();
        private readonly Dictionary<string, MethodInfo> _commandMap = RouterMethodProvider.GetRoutedMethods(typeof(RestaurantService));
        public static FoodKartService GetInstance()
        {
            return new FoodKartService();
        }
        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Write Your Command");
                    var input = Console.ReadLine();
                    if (IsExitCommand(input)) return;
                    var command = new Command(input);
                    if (_commandMap.TryGetValue(command.Name, out var method))
                    {
                        method.Invoke(_restaurantService, [command]);
                    }
                    else
                    {
                        Logger.Warn($"{command.Name} is not know command");
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message);
                }
            }
        }

        private bool IsExitCommand(string command)
        {
            return command.ToLower() == "exit";
        }
    }
}
