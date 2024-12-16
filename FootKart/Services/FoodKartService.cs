using FootKart.Interfaces;
using FootKart.Models;
using FootKart.Util;

namespace FootKart.Services
{
    public class FoodKartService
    {
        private IRestaurantService _restaurantService = RestaurantService.GetInstance();

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
                    switch (command.Name)
                    {
                        case "register_user":
                            _restaurantService.RegisterUser(command);
                            break;
                        case "login_user":
                            _restaurantService.Login(command);
                            break;
                        case "register_restaurant":
                            _restaurantService.AddRestaurant(command);
                            break;
                        case "update_quantity":
                            _restaurantService.UpdateQuantity(command);
                            break;
                        case "update_location":
                            _restaurantService.UpdateLocation(command);
                            break;
                        case "create_review":
                            _restaurantService.RateRestaurant(command);
                            break;
                        case "show_restaurant":
                            _restaurantService.ShowRestaurant(command);
                            break;
                        case "place_order":
                            _restaurantService.PlaceOrder(command);
                            break;
                        default:
                            Logger.Warn($"{command.Name} is not known command");
                            break;
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
