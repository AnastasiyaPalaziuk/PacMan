namespace PacMan.Logic.Abstract
{
    public interface IPlugin
    {
        string PluginName { get; }
        void Run(IMoveObject badBoy, IMoveObject man);
        IPluginHost Host { get; set; }
    }


}
