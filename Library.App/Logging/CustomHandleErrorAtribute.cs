using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.App.Logging
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext) // TODO: достать больше инфы из исключения
        {
            LibraryLogger.logger.Error(filterContext.Exception, filterContext.Exception.Message);
            base.OnException(filterContext);
        }
    }
}