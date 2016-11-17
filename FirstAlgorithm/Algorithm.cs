using PacMan.Logic.Abstract;

namespace FirstAlgorithm
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

        public string PluginName { get; } = "FirstAlgorithm";

        public void Run(IMoveObject badBoy, IMoveObject man)
        {
            if (badBoy.CurrentCoordinateX != man.CurrentCoordinateX)
            {
                if (badBoy.CurrentCoordinateX >man.CurrentCoordinateX)
                {
                    if (!badBoy.StepLeft())
                    {
                        badBoy.StepDown();
                    }
                }
                else
                {
                    if (!badBoy.StepRight())
                        badBoy.StepUp();
                }
            }
            else if (badBoy.CurrentCoordinateY != man.CurrentCoordinateY)
            {
                if (badBoy.CurrentCoordinateY > man.CurrentCoordinateY)
                {
                    if (!badBoy.StepUp())
                    {
                       badBoy.StepLeft();
                    }
                }
                else
                {
                    if (!badBoy.StepDown())
                        badBoy.StepRight();
                }
            }

        }
    }
}
