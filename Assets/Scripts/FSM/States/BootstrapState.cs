using System;
using FSM.Data;
using FSM.Interfaces;

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
            _stateMachine.GoMainMenu(CurtainType.BlackFade);
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