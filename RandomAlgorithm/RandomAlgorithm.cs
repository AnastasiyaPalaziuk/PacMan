using PacMan.Logic.Model;
using PacMan.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomAlgorithm
{
    public class RandomAlgorithm : IPlugin
    {
        private string _PluginName = "RandomAlgorithm";

        public IPluginHost Host { get; set;}

        public string PluginName
        {
            get
            {
                return _PluginName;
            }
        }

        public void Algorithm(BadBoy badBoy, Man man)
        {
            switch ((new Random()).Next(4) )
            {
                case 0:
                    badBoy.StepDown();
                    break;
                case 1:
                    badBoy.StepLeft();
                    break;
                case 2:
                    badBoy.StepRight();
                    break;
                case 3:
                    badBoy.StepUp();
                    break;

            }
        }
    }
}

