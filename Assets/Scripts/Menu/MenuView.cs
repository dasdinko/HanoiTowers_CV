using System;
using UnityEngine;
using UnityEngine.UI;

namespace HanoiTowers.Scripts.Menu
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] InputField _input;
        [SerializeField] Text _warningText;
        [SerializeField] Button _beginButton;

        public Action<int> Begin;



        void Awake()
        {
            _input.contentType = InputField.ContentType.IntegerNumber;
            _input.text = "Введите число";
            _warningText.enabled = false;
            _beginButton.onClick.AddListener(OnBegin);
        }

        void OnBegin()
        {
            int discsCount = 2;
            int discsCap = HanoiTowersConsts.DISC_CAP_COUNT;

            int.TryParse(_input.text, out discsCount);

            if (discsCount < 2 || discsCount > discsCap)
            {
                _warningText.enabled = true;
                _warningText.text = String.Format("Введите пожалуйста число между 2 и {0} (задано только 7 цветов)", discsCap);
                _input.text = discsCount < 2 ? 2.ToString() : 7.ToString();
                return;
            }

            _warningText.text = "";
            
            Begin.Invoke(discsCount);
            
            _beginButton.gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            _beginButton.onClick.RemoveListener(OnBegin);
        }
    }
}