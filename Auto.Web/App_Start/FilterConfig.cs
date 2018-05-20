using System.Web;
using System.Web.Mvc;
using Auto.Tools.ExceptionHandling;

namespace Auto.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MvcExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
