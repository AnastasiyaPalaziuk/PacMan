using PacMan.Logic.Abstract;
using System;
namespace RandomAlgorithm
{
   public class Algorithm : IPlugin
    {
        private IPluginHost _host;
        public IPluginHost Host
        {
            get { return _host; }
            set
            {
                _host = value;
                _host.Register(this);
            }
        }

        public string PluginName { get; } = "RandomAlgorithm";
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
