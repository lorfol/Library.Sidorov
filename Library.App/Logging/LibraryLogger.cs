using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.App.Logging
{
    internal class LibraryLogger
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
    }
}