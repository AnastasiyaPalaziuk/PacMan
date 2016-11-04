using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomAlgorithm
{
   public class Algorithm : IPlugin
    {
        private string _PluginName = "RandomAlgorithm";
        IPluginHost _Host;
        public IPluginHost Host
        {
            get { return _Host; }
            set
            {
                _Host = value;
                _Host.Register(this);
            }
        }
        public string PluginName
        {
            get
            {
                return _PluginName;
            }
        }

        public void Run(IMoveObject badBoy, IMoveObject man)
        {
            switch ((new Random()).Next(4))
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
