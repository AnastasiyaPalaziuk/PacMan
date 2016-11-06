using NLog;
using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Logic.Concrete
{
    public class LoaderPlugins : IPluginHost
    {
        private List<IPlugin> _plugins;
        private Logger log = LogManager.GetCurrentClassLogger(); 
        public List<IPlugin> Plugins
        {
            get { return _plugins; }
            private set { _plugins = value; }
        }
        public LoaderPlugins()
        {
            Loading(Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]));
        }
        private void Loading(string path)
        {
            string[] pluginFiles = Directory.GetFiles(path, "*.dll");
            this._plugins = new List<IPlugin>();

            foreach (string pluginPath in pluginFiles)
            {
                Type objType = null;
                try
                {
                    Assembly assembly = Assembly.LoadFrom(pluginPath);
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
                        log.Info("Загрузка плагина {0}", ((IPlugin)Activator.CreateInstance(objType)).PluginName);
                        this._plugins.Add((IPlugin)Activator.CreateInstance(objType));
                        this._plugins[this._plugins.Count - 1].Host = this;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        public bool Register(IPlugin plug)
        {
            return true;
        }
    }
}
