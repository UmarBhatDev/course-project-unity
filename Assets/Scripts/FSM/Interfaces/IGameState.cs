using Cysharp.Threading.Tasks;

namespace FSM.Interfaces
{
    public interface IStateBase
    {
        void Exit();
    }
    
    public interface IGameState : IStateBase
    {
        void Enter();
    }

    public interface IGameState<in TPayload> : IStateBase
        where TPayload : struct
    {
        UniTaskVoid Enter(TPayload payload);
    }
}
