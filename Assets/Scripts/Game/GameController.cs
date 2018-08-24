using HanoiTowers.Scripts.Game.Components;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace HanoiTowers.Scripts.Game
{
    public class GameController : MonoBehaviour
    {
        public GameFieldComponent GameFieldComponent;
        public GameView GameView;

        int testCount = 5; // If you are started GameScene without Menu
        
       
        

        void Start()
        {
            int discsCount = GameModel.Instance.DiscsCount;

            if (discsCount == 0) 
                discsCount = testCount;
            
            GameFieldComponent.Init(discsCount);
            GameFieldComponent.CompleteHanoiTowersTask();
            
            GameFieldComponent.TaskComplete += OnTaskCompleted;
            GameView.Restart += OnRestart;

        }

        void OnRestart()
        {
            SceneManager.LoadScene("Menu");
        }

        void OnTaskCompleted()
        {
            GameView.ShowComplete();
        }

        void OnDestroy()
        {
            // in C# 4.6 i use GameFieldComponent?.TaskComplete -= OnTaskCompleted; instead
            if(GameFieldComponent.TaskComplete != null)
                GameFieldComponent.TaskComplete -= OnTaskCompleted;
            
            if(GameView.Restart != null)
                GameView.Restart -= OnRestart;
        }
    }

    
}
