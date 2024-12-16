using FoodKart.Constants;
using FoodKart.Interfaces;
using FoodKart.Models;
using FoodKart.Util;

namespace FoodKart.Services
{
    public class RestaurantService : IRestaurantService
    {
        private List<Restaurant> _restaurants = new List<Restaurant>();
        private IUserProvider _userProvider = UserProvider.GetInstance();
        private List<Order> _orders = new List<Order>();
        private static readonly object LockObject = new object();

        private static IRestaurantService _instance;
        public static IRestaurantService GetInstance()
        {
            lock (LockObject)
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        _instance = new RestaurantService();
                    }
                }
            }
            return _instance;
        }

        public void RegisterUser(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command,4);

            var name = GetValidName(parameters[0], "UserName");
            var gender = GetValidName(parameters[1], "Gender");
            var phoneNumber = GetValidName(parameters[2], "PhoneNumber");
            var pincode = GetValidName(parameters[3], "pincode");

            if (_userProvider.IsExist(phoneNumber))
                throw new Exception("Already register with this phone number");

            _userProvider.RegisterUser(new User(name, gender, phoneNumber, pincode));
            Logger.Success($"Register user with user id {phoneNumber}");
        }

        public void Login(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var userId = GetValidName(parameters[0], "UserId");
            _userProvider.Login(userId);
            Logger.Success($"Logged in user with user id {userId}");
        }

        public void Logout()
        {
            _userProvider.Logout();
            Logger.Success($"Logged out user");

        }

        public void UpdateLocation(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var name = GetValidName(parameters[0], "RestaurantName");
            var pincodes = GetValidName(parameters[1], "pincodes");

            var restaurant = GetRestaurantByName(name);
            restaurant.SetLocations(pincodes);
            Logger.Success($"Update service location of {name} restaurant to {pincodes}");
        }
        public void AddRestaurant(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var name = GetValidName(parameters[0], "RestaurantName");
            var pincode = GetValidName(parameters[1], "pincode");
            var foodItemName = GetValidName(parameters[2], "FoodItemName");
            var foodItemPrice = double.TryParse(parameters[3], out var price)
                ? price
                : throw new Exception($"Can not parse food item price {parameters[3]} as double type");
            var quantity = int.TryParse(parameters[4], out var qun)
                ? qun
                : throw new Exception($"Can not parse food item qunatity {parameters[4]} as int type");


            var restaurant = new Restaurant(name, pincode, foodItemName, foodItemPrice, quantity);
            if (_restaurants.Exists(x => x.Name == restaurant.Name))
                throw new Exception("Already exist restaurant with this name");
            _restaurants.Add(restaurant);
            Logger.Success($"Register restaurant with name {name}");
        }

        private string GetValidName(string input, string paramName)
        {
            if (string.IsNullOrEmpty(input)) throw new Exception($"Invalid parameter: {paramName}");
            if (input.FirstOrDefault() != '\"' && input.LastOrDefault() != '\"')
                throw new Exception($"{input} is not valid string, please quoted by \"");

            var res = "";
            for (int i = 1; i < input.Length - 1; i++)
            {
                res += input[i];
            }
            return res;
        }

        private void ValidateNumberOfParameters(Command command, int expectedNumberOfParameters)
        {
            if (command.Parameters.Count != expectedNumberOfParameters)
                throw new Exception(
                    $"Expected {expectedNumberOfParameters} parameters for command {command.Name} but given {command.Parameters.Count}");
        }
        public void UpdateQuantity(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var restaurantName = GetValidName(parameters[0], "RestaurantName");
            var quantity = int.TryParse(parameters[1], out var qun)
                ? qun
                : throw new Exception($"Can not parse food item qunatity {parameters[1]} as int type");

            if (quantity < 0) throw new Exception("Invalid quantity");

            var restaurant = GetRestaurantByName(restaurantName);
            restaurant.Quantity += quantity;
            Logger.Success($"Update quantity of restaurant {restaurantName} with {quantity}");
        }

        public void RateRestaurant(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var restaurantName = GetValidName(parameters[0], "RestaurantName");
            var rating = int.TryParse(parameters[1], out var rate)
                ? rate
                : throw new Exception($"Can not parse rating {parameters[1]} as int type");
            var comment = parameters[2];

            var restaurant = GetRestaurantByName(restaurantName);
            restaurant.AddReview(rating, comment, _userProvider.GetCurrentUser());
            Logger.Success($"Add your review to restaurant {restaurantName}");
        }

        private Restaurant GetRestaurantByName(string restaurantName)
        {
            var restaurant = _restaurants.FirstOrDefault(x => x.Name == restaurantName);
            if (restaurant == null) throw new Exception($"No restaurant found with this name {restaurantName}");
            return restaurant;
        }

        public void ShowRestaurant(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var user = _userProvider.GetCurrentUser();
            var orderType = GetValidName(parameters[0], "OrderType");
            var results = new List<Restaurant>();
            foreach (var restaurant in _restaurants)
            {
                if (!restaurant.IsCoverArea(user.Pincode) || !restaurant.HasItem()) continue;
                results.Add(restaurant);
            }

            Func<Restaurant, object> sortKey = orderType.ToLower() switch
            {
                RestaurantOrderTypeConstant.Price => r => r.FoodItemPrice,
                RestaurantOrderTypeConstant.Rating => r => r.Rating,
                _ => r => r.FoodItemName
            };

            var headers = new List<string> { "Restaurant name", "Food Item Name", "Price", "Rating" };
            var data = new List<List<string>>();
            foreach (var result in results)
            {
                data.Add(new List<string>
                {
                    result.Name,result.FoodItemName,result.FoodItemPrice.ToString(),result.Rating.ToString()
                });
            }

            Logger.WriteTable(headers, data);

        }

        public void PlaceOrder(Command command)
        {
            var parameters = command.Parameters;
            ValidateNumberOfParameters(command, 4);

            var restaurantName = GetValidName(parameters[0], "RestaurantName");
            var quantity = int.TryParse(parameters[1], out var qun)
                ? qun
                : throw new Exception($"Can not parse food item quantity {parameters[1]} as int type");

            var restaurant = GetRestaurantByName(restaurantName);
            if (restaurant.Quantity < quantity) throw new Exception("Insufficient product request");

            var user = _userProvider.GetCurrentUser();
            _orders.Add(new Order(restaurant.Name, quantity, user.PhoneNumber));
            Logger.Success($"You order placed, please order. OrderId: {_orders.LastOrDefault()!.Id}");
        }

        public List<Order> GetOrderHistoryByUser()
        {
            var user = _userProvider.GetCurrentUser();
            var orders = _orders.Where(x => x.UserId == user.PhoneNumber).ToList();

            return orders;
        }
    }
}
