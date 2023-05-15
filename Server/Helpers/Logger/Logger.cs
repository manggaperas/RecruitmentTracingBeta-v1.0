using log4net;
using log4net.Config;

namespace logger
{
    static class Logger
    {
        public static void Config()
        {
            XmlConfigurator.Configure(new FileInfo("Helpers/Logger/log4net.config"));
        }
    }
}