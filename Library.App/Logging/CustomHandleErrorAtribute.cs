using System.Web.Mvc;

namespace Library.App.Logging
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        // custom attribute that write information about exeption into log file
        public override void OnException(ExceptionContext filterContext)
        {
            LibraryLogger.logger.Error(filterContext.Exception, filterContext.Exception.Message);
            base.OnException(filterContext);
        }
    }
}