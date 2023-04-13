using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Sort.SortOrder;

namespace SMTstock.Entities.Utilities.Request
{
    public class RequestForGetAllOrder
    {
        public FilterFieldForOrder? filterOrder{ get; set; }
        public string? searchString { get; set; }
        public SortFieldForOrder? sortOrder { get; set; }
        public int page { get; set; } = 1;
    }
}
