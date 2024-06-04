using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.SavePointMenu.Views
{
    public class MoveToLocationElementView : MonoBehaviour
    {
        public event Action OnGoToLocationButtonPressed;
        
        [SerializeField] private Image _locationImage;
        [SerializeField] private Text _locationNameText;
        [SerializeField] private Button _goToLocationButton;

        private void Start()
        {
            _goToLocationButton.onClick.AddListener(() => OnGoToLocationButtonPressed?.Invoke());
        }

        public void Setup(string locationNameText, Sprite locationIcon, bool buttonActive)
        {
            _locationImage.sprite = locationIcon;
            _locationNameText.text = locationNameText;

            _goToLocationButton.interactable = buttonActive;
        }
    }
}