
using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Sort;
using SMTstock.Entities.Utilities.Sort.SortProduct;

namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, params string[] message) : base(data, true, message)
        {
        }

        public SuccessDataResult(T data, PagedList pageList, params string[] message) : base(data, pageList, true,message)
        {
        }

        public SuccessDataResult(params string[] message) : base(default, true, message)
        {

        }

        public SuccessDataResult
            (
                T data, IFilterFields? filter,
                string? searchString, ISortField? sort,
                 PagedList pageList, params string[] message
            ) : base(data,true, filter, searchString, sort, pageList, message)
        {
            
        }

    }
}
