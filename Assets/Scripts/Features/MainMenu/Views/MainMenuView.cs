using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Features.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _playerButton;
        [SerializeField] private Button _exitButton;

        public event Action PlayButtonPressed;
        public event Action ExitButtonPressed;

        private CompositeDisposable _compositeDisposable;

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();
            
            _playerButton.OnClickAsObservable().Subscribe(_ =>
            {
                PlayButtonPressed?.Invoke();
            })
            .AddTo(_compositeDisposable);
            
            _exitButton.OnClickAsObservable().Subscribe(_ =>
            {
                ExitButtonPressed?.Invoke();
            })
            .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            Destroy(gameObject);
            _compositeDisposable?.Dispose();
        }
    }
}
