
using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Sort.SortProduct;

namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, params string[] message) : base(data, true,message)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(params string[] message) : base(default, true, message)
        {

        }

        public SuccessDataResult
            (
                T data, FilterFieldForProduct? filterProduct,
                string? searchString, SortFieldForProduct? sortProduct,
                 PagedList pageList, params string[] message
            ) : base(data,true, filterProduct, searchString, sortProduct, pageList, message)
        {
            
        }

    }
}
