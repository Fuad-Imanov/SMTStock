
using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class Result : IResult
    {
      

        public Result(bool success, params string[] message) : this(success)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }
        public Result(bool success,FilterFieldForProduct? filterProduct, string? searchString, SortFieldForProduct? sortProduct, PagedList pageList, params string[] message) :this(success,message)
            
        {
            Success = success;
            FilterProduct = filterProduct;
            SearchString = searchString;
            SortProduct = sortProduct;
            PageList = pageList;
            Message= message;
        }

     

        public bool Success { get; }
        public string[] Message { get; }



        public SortFieldForProduct? SortProduct { get;  }
        public FilterFieldForProduct? FilterProduct { get;  }
        public string? SearchString { get;   }
        public PagedList PageList { get;} 
    }
}
