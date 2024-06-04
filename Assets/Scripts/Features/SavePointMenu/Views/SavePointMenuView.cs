using System;
using System.Linq;
using Features.Persistence.Services;
using Features.Roadmap.Data;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.SavePointMenu.Views
{
    public class SavePointMenuView : MonoBehaviour
    {
        public MoveToLocationPanelView MoveToLocationPanelView => _moveToLocationPanelView;

        [SerializeField] private Button _resumeGameButton;
        [SerializeField] private Button _workbenchButton;
        [SerializeField] private Button _moveToAnotherLocationButton;
        [SerializeField] private Button _exitToMainMenuButton;
        [SerializeField] private MoveToLocationPanelView _moveToLocationPanelView;

        [Inject] private JourneyProgress _journeyProgress;

        public event Action OnResumeButtonPressed;
        public event Action OnWorkbenchButtonPressed;
        public event Action OnMoveToAnotherLocationButtonPressed;
        public event Action OnExitToMainMenuButtonPressed;

        private CompositeDisposable _compositeDisposable;

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();

            var stageStatuses = _journeyProgress.GetStageStatuses();

            if (stageStatuses.Count == 0) _workbenchButton.gameObject.SetActive(false);
            else
            {
                _workbenchButton
                    .OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        OnWorkbenchButtonPressed?.Invoke();
                        // SetButtonsInteractable(false);
                    })
                    .AddTo(_compositeDisposable);
            }

            if (stageStatuses.All(x => x.Value == StageStatus.Visited)) _resumeGameButton.gameObject.SetActive(false);
            else
            {
                _resumeGameButton
                    .OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        OnResumeButtonPressed?.Invoke();
                        SetButtonsInteractable(false);
                    })
                    .AddTo(_compositeDisposable);
            }

            _moveToAnotherLocationButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    OnMoveToAnotherLocationButtonPressed?.Invoke();
                    SetButtonsInteractable(false);
                })
                .AddTo(_compositeDisposable);
            _exitToMainMenuButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    OnExitToMainMenuButtonPressed?.Invoke();
                    SetButtonsInteractable(false);
                })
                .AddTo(_compositeDisposable);
        }

        public void SetButtonsInteractable(bool interactable)
        {
            _resumeGameButton.interactable = interactable;
            _workbenchButton.interactable = interactable;
            _exitToMainMenuButton.interactable = interactable;
            _moveToAnotherLocationButton.interactable = interactable;
        }

        public void Dispose()
        {
            Destroy(gameObject);
            _compositeDisposable?.Dispose();
        }
    }
}