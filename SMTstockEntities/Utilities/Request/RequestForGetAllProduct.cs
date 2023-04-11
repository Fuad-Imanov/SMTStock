using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Sort.SortProduct;

namespace SMTstock.Entities.Utilities.Request
{
    public class RequestForGetAllProduct
    {

        public FilterFieldForProduct? filterProduct { get; set; }
        public string? searchString { get; set; }
        public SortFieldForProduct? sortProduct { get; set; }
        public int page { get; set; } = 1;

    }
}
