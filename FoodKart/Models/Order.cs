namespace FoodKart.Models
{
    public class Order
    {
        public Order(string restaurantName, int quantity, string userId)
        {
            Id = Guid.NewGuid().ToString();
            RestaurantName = restaurantName;
            Quantity = quantity;
            UserId = userId;
        }

        public string Id { get; set; }
        public string RestaurantName { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}
