
using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Sort;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, params string[] message) : base(data, false, message)
        {
        }

        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(T data, PagedList pageList, params string[] message):base(data,pageList,false,message)
        {

        }

        public ErrorDataResult(params string[] message) : base(default, false,message)
        {

        }

        public ErrorDataResult() : base(default, false)
        {

        }

        public ErrorDataResult
            (
                T data, IFilterFields? filter,
                string? searchString, ISortField? sort,
                 PagedList pageList, params string[] message
            ) : base(data, false, filter, searchString, sort, pageList, message)
        {

        }
    }
}
