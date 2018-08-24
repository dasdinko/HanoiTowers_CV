namespace HanoiTowers.Scripts
{
    public class GameModel
    {
        static GameModel _instance;

        GameModel()
        {
        }

        public static GameModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameModel();
                }

                return _instance;
            }
        }

        public int DiscsCount;
    }
}