
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
    }
}
