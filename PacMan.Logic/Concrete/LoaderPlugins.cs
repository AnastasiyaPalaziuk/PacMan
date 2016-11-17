using NLog;
using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PacMan.Logic.Concrete
{
    public class LoaderPlugins : IPluginHost
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger(); 
        public List<IPlugin> Plugins { get; private set; }

        public LoaderPlugins()
        {
            Loading(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
        }
        private void Loading(string path)
        {
            var pluginFiles = Directory.GetFiles(path, "*.dll");
            Plugins = new List<IPlugin>();

            foreach (var pluginPath in pluginFiles)
            {
                Type objType = null;
                try
                {
                    var assembly = Assembly.LoadFrom(pluginPath);
                    if (assembly != null)
                    {
                        objType = assembly.GetType(Path.GetFileNameWithoutExtension(pluginPath) + ".Algorithm");
                    }
                }
                catch
                {
                    continue;
                }
                try
                {
                    if (objType != null)
                    {
                        _log.Info("Загрузка плагина {0}", ((IPlugin)Activator.CreateInstance(objType)).PluginName);
                        Plugins.Add((IPlugin)Activator.CreateInstance(objType));
                        Plugins[Plugins.Count - 1].Host = this;
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        public bool Register(IPlugin plug)
        {
            return true;
        }
    }
}
