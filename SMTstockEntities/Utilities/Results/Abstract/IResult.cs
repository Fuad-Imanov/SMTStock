﻿using SMTstock.Entities.Utilities.Filter;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Results.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string[] Message { get; }
    }
}
