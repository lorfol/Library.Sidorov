using Library.App.Logging;
using System.Web.Mvc;

namespace Library.App
{
    public class FilterConfig
    {
        // register filters for logging
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new LogHttpRequest());
        }
    }
}
