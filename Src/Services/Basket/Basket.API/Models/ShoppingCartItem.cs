namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}