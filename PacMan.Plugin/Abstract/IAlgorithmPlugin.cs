using PacMan.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Plugin
{
    public interface IPlugin
    {
        string PluginName { get; } 
        void Algorithm(BadBoy badBoy,Man man);
        IPluginHost Host { get; set; } 
    }
}
