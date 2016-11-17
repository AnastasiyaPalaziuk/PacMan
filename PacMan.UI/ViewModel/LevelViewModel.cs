namespace PacMan.UI.ViewModel
{
    public class LevelViewModel
    {
        private readonly int _level=1;

        public LevelViewModel(int level)
        {
            _level = level;
        }

        public LevelViewModel()
        {
        }

        public string DisplayLevel => "Level: " + _level;
    }
}
