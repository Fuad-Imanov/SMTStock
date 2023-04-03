using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Balance { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
    }
}
