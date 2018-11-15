using NLog;

namespace Library.App.Logging
{
    internal class LibraryLogger
    {
        // initialize NLog logger
        public static Logger logger = LogManager.GetCurrentClassLogger();
    }
}