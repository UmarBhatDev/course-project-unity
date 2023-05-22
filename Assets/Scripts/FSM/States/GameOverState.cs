using Bootstrap.GlobalDisposable.Services;
using Cysharp.Threading.Tasks;
using Features.GameOverMenu.Factories;
using Features.SceneTransitions;
using Features.SceneTransitions.Factories;
using FSM.Data;
using FSM.Interfaces;
using UnityEngine;

namespace FSM.States
{
    public class GameOverState : IGameState<GameOverState.PayLoad>
    {
        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;
        private readonly GameOverControllerFactory _gameOverControllerFactory;

        public GameOverState(GameOverControllerFactory gameOverControllerFactory, 
            CurtainViewFactory curtainViewFactory, GlobalCompositeDisposable globalCompositeDisposable)
        {
            _gameOverControllerFactory = gameOverControllerFactory;
            _globalCompositeDisposable = globalCompositeDisposable;
            _curtainViewFactory = curtainViewFactory;
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            var curtainType = payload.CurtainOverride ?? CurtainType.BlackFade;

            await Transition.ToScene("GameOverMenu", _curtainViewFactory, _globalCompositeDisposable, curtainType);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            var gameOverController = _gameOverControllerFactory.Create();

            gameOverController.Initialize();
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
        public static void GoGameOver(this IStateMachine stateMachine, CurtainType? curtainType = null)
            => stateMachine.EnterState<GameOverState, GameOverState.PayLoad>(new GameOverState.PayLoad
            {
                CurtainOverride = curtainType
            });
    }
}