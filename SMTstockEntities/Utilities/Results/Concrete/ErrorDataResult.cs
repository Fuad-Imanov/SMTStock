
using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
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

        public ErrorDataResult(params string[] message) : base(default, false,message)
        {

        }

        public ErrorDataResult() : base(default, false)
        {

        }

        public ErrorDataResult
            (
                T data, FilterFieldForProduct? filterProduct,
                string? searchString, SortFieldForProduct? sortProduct,
                 PagedList pageList, params string[] message
            ) : base(data, false, filterProduct, searchString, sortProduct, pageList, message)
        {

        }
    }
}
