using FoodKart.Models;

namespace FoodKart.Interfaces
{
    public interface IUserProvider
    {
        void Login(string userId);
        void RegisterUser(User user);
        void Logout();
        User GetCurrentUser();
        bool IsExist(string userId);
        string GetUserPincode();
    }
}
