using System;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Auto.Tools.ExceptionHandling
{
    public class WebApiExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            try
            {
                var httpContext = (HttpContextBase) context.Request.Properties["MS_HttpContext"];
                // Log exception
            }
            catch (Exception e)
            {
                // Log exception
            }
        }
    }
}