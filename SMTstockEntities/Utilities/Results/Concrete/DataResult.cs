using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.Utilities.Sort;
using SMTstock.Entities.Utilities.Sort.SortProduct;

namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success, params string[] message) : base(success, message)
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public DataResult(T data,PagedList pageList,bool success, params string[] message) :base(pageList,success,message)
        {
            Data = data;
        }

        public DataResult
            (
                T data, bool success, IFilterFields? filter,
                string? searchString, ISortField? sort,
                PagedList pageList, params string[] message
            ) : base(success, filter, searchString, sort, pageList, message)
        {
            Data = data;
        }
        public T Data { get; }


    }
}
