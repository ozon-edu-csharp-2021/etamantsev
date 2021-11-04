using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OzonEdu.MerchandiseService.Middleware.Filters
{
    public class AppExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var resultObject = new
            {
                ExceptionType = context.Exception.GetType().FullName,
                context.Exception.Message,
                context.Exception.StackTrace
                
            };

            var jsonResult = new JsonResult(resultObject)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.Result = jsonResult;
        }
    }
}