using System;
using FSM.Data;
using FSM.Interfaces;
using UnityEngine.SceneManagement;

namespace FSM.States
{
    public class BootstrapState : IGameState
    {
        private readonly IStateMachine _stateMachine;

        public BootstrapState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            StartGame();
        }

        private void StartGame()
        {
            var activeScene = SceneManager.GetActiveScene();

            switch (DeduceSceneType(activeScene))
            {
                case SceneType.Journey:
                    _stateMachine.GoJourney(CurtainType.BlackFade);
                    break;
                case SceneType.Loader:
                    // _stateMachine.GoLoader(new LoaderState.Payload() { LoadingMode = LoaderState.LoadingMode.launch });
                    break;
                case SceneType.MainMenu:
                    _stateMachine.GoMainMenu(CurtainType.BlackFade);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static SceneType DeduceSceneType(Scene scene)
            => scene.name switch
            {
                "MainMenu" => SceneType.MainMenu,
                "LoaderScene" => SceneType.Loader,
                _ => SceneType.Journey
            };


        private enum SceneType
        {
            Loader,
            Journey,
            MainMenu
        }

        public void Exit()
        {
        }
    }

    public static partial class StateMachineExtensions
    {
        public static void GoBootstrap(this IStateMachine stateMachine)
            => stateMachine.EnterState<BootstrapState>();
    }
}