using System;
using Features.Persistence.Services;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.MainMenu.Views
{
    public class MainMenuView : MonoBehaviour, IDisposable
    {
        [SerializeField] private Button _resumeGameButton;
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _exitButton;

        [Inject] private JourneyProgress _journeyProgress;
        
        public event Action PlayButtonPressed;
        public event Action ExitButtonPressed;

        private CompositeDisposable _compositeDisposable;

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();

            var activeStage = _journeyProgress.GetActiveStage();
            
            if (activeStage == null)
            {
                _resumeGameButton.gameObject.SetActive(false);
                _newGameButton.gameObject.SetActive(true);
                _exitButton.gameObject.SetActive(true);
            }
            else
            {
                _resumeGameButton.gameObject.SetActive(true);
                _newGameButton.gameObject.SetActive(true);
                _exitButton.gameObject.SetActive(true);
            }

            _newGameButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    PlayerPrefs.DeleteAll();
                    PlayButtonPressed?.Invoke();

                    SetButtonsInteractable(false);
                })
                .AddTo(_compositeDisposable);

            _resumeGameButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    PlayButtonPressed?.Invoke(); 
                    SetButtonsInteractable(false);
                })
                .AddTo(_compositeDisposable);

            _exitButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    ExitButtonPressed?.Invoke(); 
                    SetButtonsInteractable(false);
                })
                .AddTo(_compositeDisposable);
        }

        private void SetButtonsInteractable(bool interactable)
        {
            _resumeGameButton.interactable = interactable;
            _newGameButton.interactable = interactable;
            _exitButton.interactable = interactable;
        }
        
        public void Dispose()
        {
            Destroy(gameObject);
            _compositeDisposable?.Dispose();
        }
    }
}
