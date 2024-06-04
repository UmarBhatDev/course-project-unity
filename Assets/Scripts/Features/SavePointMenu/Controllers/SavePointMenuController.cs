using System;
using Bootstrap.CanvasBootstrap.Data;
using Cysharp.Threading.Tasks;
using Features.SavePointMenu.Factories;
using Features.SavePointMenu.Views;
using FSM;
using FSM.Data;
using FSM.States;
using UnityEngine.Scripting;

namespace Features.SavePointMenu.Controllers
{
    [Preserve]
    public class SavePointMenuController : IDisposable
    {
        private readonly ViewRegistry _viewRegistry;
        private readonly IStateMachine _stateMachine;
        private readonly SavePointMenuViewFactory _savePointMenuViewFactory;

        private SavePointMenuView _savePointMenuView;

        public SavePointMenuController(SavePointMenuViewFactory savePointMenuViewFactory, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _savePointMenuViewFactory = savePointMenuViewFactory;
        }

        public void Initialize()
        {
            _savePointMenuView = _savePointMenuViewFactory.Create();

            _savePointMenuView.OnResumeButtonPressed += () =>
            {
                _stateMachine.GoJourney(curtainType: CurtainType.BlackFade, task: Close);
            };
            _savePointMenuView.OnMoveToAnotherLocationButtonPressed += () => OpenMoveToAnotherLocationMenu().Forget();
            _savePointMenuView.OnExitToMainMenuButtonPressed += () =>
            {
                _stateMachine.GoMainMenu(CurtainType.BlackFade, Close);
            };
        }

        private async UniTask OpenMoveToAnotherLocationMenu()
        {
            var moveToLocationPanelView = _savePointMenuView.MoveToLocationPanelView;

            moveToLocationPanelView.gameObject.SetActive(true);

            var playerInputCompletionSource = new UniTaskCompletionSource<(bool isExitClicked, string chosenStageId)>();

            moveToLocationPanelView.OnLocationChosen += chosenStageId =>
                playerInputCompletionSource.TrySetResult((false, chosenStageId));
            moveToLocationPanelView.OnCloseButtonClicked += () =>
                playerInputCompletionSource.TrySetResult((true, string.Empty));

            var result = await playerInputCompletionSource.Task;

            if (result.isExitClicked)
            {
                moveToLocationPanelView.gameObject.SetActive(false);
                _savePointMenuView.SetButtonsInteractable(true);
            }
            else if (result.chosenStageId != string.Empty)
            {
                _stateMachine.GoJourney(overrideActiveStage: result.chosenStageId, curtainType: CurtainType.BlackFade, task: Close);
            }
        }

        private UniTask Close()
        {
            Dispose();
            return UniTask.CompletedTask;
        }
        
        public void Dispose()
        {
            _savePointMenuView.Dispose();
        }
    }
}