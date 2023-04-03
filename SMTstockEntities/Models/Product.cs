using System.ComponentModel.DataAnnotations;

namespace SMTstock.Entities.Models
{
    public class Product
    {
        public Product()
        {
            OrdersProducts = new HashSet<OrderProduct>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Balance { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Price { get; set; }

       
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<OrderProduct> OrdersProducts { get; set; }

    }
}
