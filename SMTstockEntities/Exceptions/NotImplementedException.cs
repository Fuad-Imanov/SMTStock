using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Exceptions
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string message) : base(message)
        { }
    }
}
