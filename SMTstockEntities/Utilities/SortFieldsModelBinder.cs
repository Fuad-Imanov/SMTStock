using Microsoft.AspNetCore.Mvc.ModelBinding;
using SMTstock.Entities.Utilities.Sort.SortProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SMTstock.Entities.Utilities
{
    public class SortFieldsModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(new List<SortFieldForProduct>());
                return;
            }

            //var json = valueProviderResult.FirstValue;
            //if (string.IsNullOrEmpty(json))
            //{
            //    bindingContext.Result = ModelBindingResult.Success(new List<SortFieldForProduct>());
            //    return;
            //}

            try
            {
                var result = new List<SortFieldForProduct>();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                foreach(var json in valueProviderResult.Values)
                {
                    var jsonResult = JsonSerializer.Deserialize<SortFieldForProduct>(json, options);
                    result.Add(jsonResult);
                }
                

                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch (JsonException ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex, bindingContext.ModelMetadata);
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }

}
