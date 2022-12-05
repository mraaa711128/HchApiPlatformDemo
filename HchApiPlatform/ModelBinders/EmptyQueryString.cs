using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HchApiPlatform.ModelBinders
{
    public class EmptyQueryStringModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (result == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Success(string.Empty);
            } else
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, result);
                var rawValue = result.FirstValue;
                if (string.IsNullOrEmpty(rawValue))
                {
                    bindingContext.Result = ModelBindingResult.Success(string.Empty);
                } else
                {
                    bindingContext.Result = ModelBindingResult.Success(rawValue);
                }
            }
            return Task.CompletedTask;
        }
    }

    public class EmptyQueryStringModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(string) &&
                    context.BindingInfo.BindingSource != null &&
                    context.BindingInfo.BindingSource.CanAcceptDataFrom(BindingSource.Query))
            {
                return new EmptyQueryStringModelBinder();
            }

            return null;
        }
    }
}
