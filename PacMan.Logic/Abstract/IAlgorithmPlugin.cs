using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Logic.Abstract
{
    public interface IPlugin
    {
        string PluginName { get; } 
        void Run(IMoveObject badBoy,IMoveObject man);
        IPluginHost Host { get; set; } 
    }
}
