using System;
using Cysharp.Threading.Tasks;
using Features.MainMenu.Factories;
using Features.SceneTransitions.Factories;
using FSM.Data;
using FSM.Interfaces;

namespace FSM.States
{
    public class MainMenuState : IGameState<MainMenuState.PayLoad>
    {
        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly MainMenuControllerFactory _mainMenuControllerFactory;

        public MainMenuState(MainMenuControllerFactory mainMenuControllerFactory, CurtainViewFactory curtainViewFactory)
        {
            _curtainViewFactory = curtainViewFactory;
            _mainMenuControllerFactory = mainMenuControllerFactory;
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            var mainMenuController = _mainMenuControllerFactory.Create();
            
            mainMenuController.Initialize();
        }
        
        public void Exit()
        {
        }
        
        public struct PayLoad
        {
            public CurtainType? CurtainOverride { get; set; }

            public PayLoad(CurtainType curtainOverride)
            {
                CurtainOverride = curtainOverride;
            }
        }
    }
    
    public static partial class StateMachineExtensions
    {
        public static void GoMainMenu(this IStateMachine stateMachine, CurtainType? curtainType = null)
            => stateMachine.EnterState<MainMenuState, MainMenuState.PayLoad>(new MainMenuState.PayLoad
            {
                CurtainOverride = curtainType
            });
    }
}