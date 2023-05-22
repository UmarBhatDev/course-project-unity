using Bootstrap.GlobalDisposable.Services;
using Cysharp.Threading.Tasks;
using Features.MainMenu.Factories;
using Features.SceneTransitions;
using Features.SceneTransitions.Factories;
using FSM.Data;
using FSM.Interfaces;
using UnityEngine;

namespace FSM.States
{
    public class MainMenuState : IGameState<MainMenuState.PayLoad>
    {
        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;
        private readonly MainMenuControllerFactory _mainMenuControllerFactory;

        public MainMenuState(MainMenuControllerFactory mainMenuControllerFactory, 
            CurtainViewFactory curtainViewFactory, GlobalCompositeDisposable globalCompositeDisposable)
        {
            _mainMenuControllerFactory = mainMenuControllerFactory;
            _globalCompositeDisposable = globalCompositeDisposable;
            _curtainViewFactory = curtainViewFactory;
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            var curtainType = payload.CurtainOverride ?? CurtainType.BlackFade;

            await Transition.ToScene("MainMenu", _curtainViewFactory, _globalCompositeDisposable, curtainType, payload.AdditionalAction);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            var mainMenuController = _mainMenuControllerFactory.Create();
            
            mainMenuController.Initialize();
        }
        
        public void Exit()
        {
        }
        
        public struct PayLoad
        {
            public AdditionalTask AdditionalAction;
            public CurtainType? CurtainOverride { get; set; }

            public PayLoad(CurtainType curtainOverride, AdditionalTask additionalAction)
            {
                CurtainOverride = curtainOverride;
                AdditionalAction = additionalAction;
            }
        }
    }
    
    public static partial class StateMachineExtensions
    {
        public static void GoMainMenu(this IStateMachine stateMachine, CurtainType? curtainType = null, AdditionalTask additionalTask = null)
            => stateMachine.EnterState<MainMenuState, MainMenuState.PayLoad>(new MainMenuState.PayLoad
            {
                CurtainOverride = curtainType,
                AdditionalAction = additionalTask
            });
    }
}