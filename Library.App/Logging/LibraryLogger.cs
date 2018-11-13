using NLog;

namespace Library.App.Logging
{
    internal class LibraryLogger
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();
    }
}