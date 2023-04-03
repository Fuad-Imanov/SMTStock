
using SMTstock.Entities.Utilities.Results.Abstract;
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
        public bool Success { get; }
        public string[] Message { get; }
    }
}
