using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WeddingSample.Filters
{
    public class CheckRefererFilter :  Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var referer = context.HttpContext.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer) && referer != "http://127.0.0.1:5500/src/index.html")
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
