using System;
using Cysharp.Threading.Tasks;
using Features.GameOverMenu.Factories;
using Features.GameOverMenu.Views;
using FSM;
using FSM.Data;
using FSM.States;

namespace Features.GameOverMenu.Controllers
{
    public class GameOverController : IDisposable
    {
        private GameOverMenuView _gameOverView;
        
        private readonly GameOverViewFactory _gameOverViewFactory;
        private readonly IStateMachine _stateMachine;

        public GameOverController(GameOverViewFactory gameOverViewFactory, IStateMachine stateMachine)
        {
            _gameOverViewFactory = gameOverViewFactory;
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _gameOverView = _gameOverViewFactory.Create();

            _gameOverView.TryAgainPressed += () =>
            {
                _stateMachine.GoJourney(curtainType: CurtainType.BlackFade, task: Close);
            };
            
            _gameOverView.ReturnToMainMenuPressed += () =>
            {
                _stateMachine.GoMainMenu(CurtainType.BlackFade, Close);
            };
            
            UniTask Close()
            {
                Dispose();
                return UniTask.CompletedTask;
            }
        }

        public void Dispose()
        {
            _gameOverView.Dispose();
        }
    }
}