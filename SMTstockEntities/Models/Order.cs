using System.ComponentModel.DataAnnotations;

namespace SMTstock.Entities.Models
{
    public class Order
    {
        public Order()
        {
            OrdersProducts = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public int MerchantId { get; set; }
        public virtual Merchant Merchant { get; set; }
        public double OrderTotalPrice { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }
        public virtual ICollection<OrderProduct> OrdersProducts { get; set; }
    }
}
