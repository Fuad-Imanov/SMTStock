using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
