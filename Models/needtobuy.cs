using System.Linq;
using System.Web;

namespace do_an_web.Models
{
    public class needtobuy
    {
        webClothesEntities buy = new webClothesEntities();
        public int id_product { get; set; }
        public string name { get; set; }
        public string images { get; set; }
        public double price { get; set; }
        public int quantity { get; set; }
        public double total()
        {
            return price * quantity;
        }

        public needtobuy(int products)
        {
            this.id_product = products;
            var product = buy.products.Single(s => s.id_products == this.id_product);
            this.name = product.name_product;
            this.images = product.images;
            this.price = double.Parse(product.price.ToString());
            this.quantity = 1;
        }
    }
}