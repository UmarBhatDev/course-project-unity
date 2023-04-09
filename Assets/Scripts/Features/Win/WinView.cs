using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Win
{
    public class WinView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _winButton;

        public Button WinButton => _winButton;

        public bool ButtonTapped;

        public void Start()
        {
            _winButton.onClick.AddListener(SetButtonTappedTrue);
        }

        private void SetButtonTappedTrue()
        {
            ButtonTapped = true;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}