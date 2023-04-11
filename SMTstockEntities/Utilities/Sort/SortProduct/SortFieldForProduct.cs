using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Sort.SortProduct
{
    public class SortFieldForProduct
    {
        public bool NameAsc { get; set; }
        public bool NameDesc { get; set; }
        public bool PriceAsc { get; set; }
        public bool PriceDesc { get; set; }
    }
}




// Product controller -de getAll metodu parametr kimi list<SortFieldForProduct> qebul etmesi ucun
//     public class SortFieldForProduct
//{
//    public SortFieldForProduct()
//    {

//    }
//    [JsonProperty]
//    public string Field { get; set; }
//    [JsonProperty]
//    public bool Desc { get; set; }
//}
