
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Results.Concrete
{
    public class ErrorResult : Result
    {
        public ErrorResult(params string[] message) : base(false, message)
        {
        }

        public ErrorResult() : base(false)
        {
        }
    }
}
