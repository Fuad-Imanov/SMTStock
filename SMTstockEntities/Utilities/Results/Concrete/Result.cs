
using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Pagination;
using SMTstock.Entities.Utilities.Results.Abstract;
using SMTstock.Entities.Utilities.Sort;
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

        public Result(PagedList pageList,bool success, params string[] message)
        {
            Success = success;
            PageList = pageList;
            Message = message;
        }
        public Result(bool success, IFilterFields? filter, string? searchString, ISortField? sort, PagedList pageList, params string[] message) :this(success,message)
            
        {
            Success = success;
            Filter = filter;
            SearchString = searchString;
            Sort = sort;
            PageList = pageList;
            Message= message;
        }

     

        public bool Success { get; }
        public string[] Message { get; }



        public ISortField? Sort { get; }
        public IFilterFields?  Filter { get; } 
        public string? SearchString { get; }
        public PagedList PageList { get; } 
    }
}
