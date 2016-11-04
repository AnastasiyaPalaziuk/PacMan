﻿using PacMan.Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAlgorithm
{
    public class Algorithm : IPlugin
    {
        private string _PluginName = "FirstAlgorithm";
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
