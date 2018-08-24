using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace HanoiTowers.Scripts.Game
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] CanvasGroup _complete;
        [SerializeField] Button _repeatButton;

        public Action Restart;


        void Start()
        {
            _complete.alpha = 0;
        }
        
        public void ShowComplete()
        {
            _complete.DOFade(1, 0.3f);
            _repeatButton.onClick.AddListener(OnRepeat);
        }

        void OnRepeat()
        {
            Restart.Invoke();
        }

        void OnDestroy()
        {
            _repeatButton.onClick.RemoveListener(OnRepeat);
        }
    }
}