using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Features.PaperHint.Views
{
    public class PaperHintView : MonoBehaviour, IDisposable
    {
        public event Action OnContinueClicked;
        
        [SerializeField] private Button _continueButton;

        private async void Start()
        {
            await UniTask.Delay(TimeSpan.FromMilliseconds(3000));
            _continueButton.onClick.AddListener(() => OnContinueClicked?.Invoke());
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
