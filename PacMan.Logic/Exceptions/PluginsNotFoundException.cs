using System;
using NLog;

namespace PacMan.Logic.Exceptions
{
    public class PluginsNotFoundException :Exception
    {
        private Logger _log = LogManager.GetCurrentClassLogger();
        public PluginsNotFoundException() : base()
        {
            _log.Error("Plugins with algorithms not found");
        }

    }
}
