using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMan.Logic.Abstract;

namespace PacMan.Logic.Concrete
{
    public static class CurrentAlgorithm
    {
        private static readonly LoaderPlugins Plugins = new LoaderPlugins();

        public static IPlugin Value { get; set; } =  Plugins.Plugins[0];
    }
}
