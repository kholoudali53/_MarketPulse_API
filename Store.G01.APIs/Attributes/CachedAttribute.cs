using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.G01.Core.Services.Contract;
using System.Text;

namespace Store.G01.APIs.Attributes
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireTime;

        public CachedAttribute(int expireTime)
        {
            _expireTime = expireTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();

            var casheKey = GenerateCasheKeyFromRequest(context.HttpContext.Request);

            var casheResponse = await casheService.GetCasheKeyAsync(casheKey);

            if (!string.IsNullOrEmpty(casheResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = casheResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;
                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult response)
            {
                await casheService.SetCasheKeyAsync(casheKey, response.Value, TimeSpan.FromSeconds(_expireTime));
            }

        }

        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            var casheKey = new StringBuilder();
            casheKey.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(X => X.Key))
            {
                casheKey.Append($"|{key}-{value}");
            }
            return casheKey.ToString();
        }
    }
}
