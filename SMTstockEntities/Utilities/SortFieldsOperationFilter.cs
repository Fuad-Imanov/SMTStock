using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities
{
    public class SortFieldsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters != null)
            {
                foreach (var parameter in operation.Parameters)
                {
                    if (parameter.Name == "sortFields")
                    {
                        parameter.Explode = true;
                        parameter.Style = ParameterStyle.DeepObject;
                    }
                }
            }
        }
    }
}
