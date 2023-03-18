namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {

        }
        public ShoppingCart(string userName)
        {
            this.UserName = userName;
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public decimal TotalPrice { 
            get {
                decimal totalPrice = 0;
                this.ShoppingCartItems.ForEach((item) => { totalPrice += item.Quantity * item.Price; });
                return totalPrice; 
            } 
        }
    }
}
