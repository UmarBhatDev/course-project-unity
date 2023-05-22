using System;
using Features.PressableButtons.Views;
using UniRx;
using UnityEngine;

namespace Features.GameOverMenu.Views
{
    public class GameOverMenuView : MonoBehaviour, IDisposable
    {
        [SerializeField] private PressableButtonBaseView _tryAgainButton;
        [SerializeField] private PressableButtonBaseView _returnToMainMenuButton;

        public event Action TryAgainPressed;
        public event Action ReturnToMainMenuPressed;

        private CompositeDisposable _compositeDisposable;
        
        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();

            _tryAgainButton
                .Interacted
                .Subscribe(_ =>
                {
                    TryAgainPressed?.Invoke();
                })
                .AddTo(_compositeDisposable);
            
            _returnToMainMenuButton
                .Interacted
                .Subscribe(_ =>
                {
                    ReturnToMainMenuPressed?.Invoke();
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            
            Destroy(gameObject);
        }
    }
}