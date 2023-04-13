using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Sort.SortOrder
{
    public class SortFieldForOrder:ISortField
    {
        public bool OrderDateAsc { get; set; }
        public bool OrderDateDesc { get; set; }
        public bool OrderTotalPriceAsc { get; set; }
        public bool OrderTotalPriceDesc { get; set; }
    }
}
