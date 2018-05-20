using System.Web.Mvc;

namespace Auto.Tools.ExceptionHandling
{
    public class MvcExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log exception
        }
    }
}