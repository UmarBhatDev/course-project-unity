using Cysharp.Threading.Tasks;
using Features.MainMenu.Factories;
using FSM.Data;
using FSM.Interfaces;
using UniRx;

namespace FSM.States
{
    public class MainMenuState : IGameState<MainMenuState.PayLoad>
    {
        private CompositeDisposable _compositeDisposable;
        private readonly MainMenuControllerFactory _mainMenuControllerFactory;

        public MainMenuState(MainMenuControllerFactory mainMenuControllerFactory)
        {
            _compositeDisposable = new CompositeDisposable();
            _mainMenuControllerFactory = mainMenuControllerFactory;
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            _compositeDisposable = new CompositeDisposable();

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