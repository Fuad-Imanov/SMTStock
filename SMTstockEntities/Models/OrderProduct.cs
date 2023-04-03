using System.ComponentModel.DataAnnotations;

namespace SMTstock.Entities.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }      
        public int OrderId { get; set; }      
        public double? TotalPrice => Quantity * Product.Price;
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}
