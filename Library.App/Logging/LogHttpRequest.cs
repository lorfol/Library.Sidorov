using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Logging
{
    public class LogHttpRequest : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LibraryLogger.logger.Debug($"Request started: {filterContext.HttpContext.Request.Url}");
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            LibraryLogger.logger.Debug($"Request finished");
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            LibraryLogger.logger.Debug("Time of current HTTP Request: " + filterContext.HttpContext.Timestamp + '\n' + "-----------------");
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            LibraryLogger.logger.Debug("Current User: " + filterContext.HttpContext.User.Identity.Name);
            base.OnResultExecuting(filterContext);
        }
    }
}