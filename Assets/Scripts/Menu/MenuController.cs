using UnityEngine;
using UnityEngine.SceneManagement;

namespace HanoiTowers.Scripts.Menu
{
    public class MenuController : MonoBehaviour
    {
        public MenuView MenuView;
        
        
        
        void Start()
        {
            MenuView.Begin += OnBegin;

        }

        void OnBegin(int discsCount)
        {
            GameModel.Instance.DiscsCount = discsCount;
            
            SceneManager.LoadScene("Game");
        }

        void OnDestroy()
        {
            if(MenuView != null)
                MenuView.Begin -= OnBegin;
        }
    }
}