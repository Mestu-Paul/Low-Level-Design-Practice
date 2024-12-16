using FoodKart.Models;

namespace FoodKart.Interfaces
{
    public interface IRestaurantService
    {
        void RegisterUser(Command command);
        void Login(Command command);
        void Logout();
        void UpdateLocation(Command command);
        void AddRestaurant(Command command);
        void UpdateQuantity(Command command);
        void RateRestaurant(Command command);
        void ShowRestaurant(Command command);
        void PlaceOrder(Command command);
        List<Order> GetOrderHistoryByUser();
    }
}
