namespace PacMan.Logic.Abstract
{
    public interface IMoveObject
    {
        int CurrentCoordinateY { get; set; }
        int CurrentCoordinateX { get; set; }
        bool StepLeft();
        bool StepRight();
        bool StepUp();
        bool StepDown();

    }
}
