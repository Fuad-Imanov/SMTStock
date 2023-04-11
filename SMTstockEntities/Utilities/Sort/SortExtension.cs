using SMTstock.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities.Sort
{
    public static class SortExtension
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, bool descending,bool ordering)
        {
            // Dynamically creates a call like this: query.OrderBy(p =&gt; p.SortColumn)
            var parameter = Expression.Parameter(typeof(T), "p");
            string command = "OrderBy";
            if (!ordering)
            {
                 command = "OrderBy";

                if (descending)
                {
                    command = "OrderByDescending";
                }
            }
            else
            {
                 command = "ThenBy";

                if (descending)
                {
                    command = "ThenByDescending";
                }
            }
            Expression resultExpression = null;

            var property = typeof(T).GetProperty(sortColumn);
            // this is the part p.SortColumn
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            // this is the part p =&gt; p.SortColumn
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // finally, call the "OrderBy" / "OrderByDescending" method with the order by lamba expression
            resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(T), property.PropertyType },
               query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
