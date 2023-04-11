using Microsoft.OpenApi.Models;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities
{
    public class SortFieldParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parametersToRemove = new List<OpenApiParameter>();
            var parametersToAdd = new List<OpenApiParameter>();

            foreach (var parameter in operation.Parameters)
            {
                if (parameter.Description != null && parameter.Description.Equals("SortFields", StringComparison.OrdinalIgnoreCase))
                {
                    parametersToRemove.Add(parameter);

                    var sortFieldsSchema = new OpenApiSchema
                    {
                        Type = "array",
                        Items = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                        {
                            { "Field", new OpenApiSchema { Type = "boolean" } },
                            { "Desc", new OpenApiSchema { Type = "boolean" } }
                        },
                            Required = new HashSet<string> { "Field" }
                        }
                    };

                    parametersToAdd.Add(new OpenApiParameter
                    {
                        Name = "SortFields",
                        In = ParameterLocation.Query,
                        Description = "Sort Fields",
                        Required = false,
                        Schema = sortFieldsSchema
                    });
                }
            }

            foreach (var parameter in parametersToRemove)
            {
                operation.Parameters.Remove(parameter);
            }

            foreach (var parameter in parametersToAdd)
            {
                operation.Parameters.Add(parameter);
            }
        }
    }

}
