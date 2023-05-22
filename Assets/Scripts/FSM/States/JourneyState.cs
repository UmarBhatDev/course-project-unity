using System;
using System.Threading;
using Bootstrap;
using Bootstrap.GlobalDisposable.Services;
using Cysharp.Threading.Tasks;
using Features.Journey.Factories;
using Features.Persistence.Services;
using Features.Roadmap.Data;
using Features.SceneTransitions;
using Features.SceneTransitions.Factories;
using FSM.Data;
using FSM.Interfaces;

namespace FSM.States
{
    public class JourneyState : IGameState<JourneyState.PayLoad>
    {
        private readonly IStateMachine _stateMachine;
        private readonly JourneyProgress _journeyProgress;
        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly JourneyControllerFactory _journeyControllerFactory;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;

        private CancellationTokenSource _stateCancellationTokenSource;

        public JourneyState(CurtainViewFactory curtainViewFactory, IStateMachine stateMachine,
            JourneyProgress journeyProgress, JourneyControllerFactory journeyControllerFactory, GlobalCompositeDisposable globalCompositeDisposable)
        {
            _stateMachine = stateMachine;
            _journeyProgress = journeyProgress;
            _curtainViewFactory = curtainViewFactory;
            _journeyControllerFactory = journeyControllerFactory;
            _globalCompositeDisposable = globalCompositeDisposable;

            _stateCancellationTokenSource = new CancellationTokenSource();
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            _stateCancellationTokenSource = new CancellationTokenSource();

            var curtainType = payload.CurtainOverride ?? CurtainType.BlackFade;
            
            var stage = _journeyProgress.GetActiveStage();
            
            if (stage == null)
            {
                _stateMachine.GoMainMenu(CurtainType.BlackFade); 
                return;
            }
            
            await Transition.ToScene(stage.SceneName, _curtainViewFactory, _globalCompositeDisposable, curtainType, payload.AdditionalAction);

            try
            {
                await Play(stage);
                FinishStage(stage);

                if (!_stateCancellationTokenSource.Token.IsCancellationRequested)
                {
                    stage = _journeyProgress.GetActiveStage();

                    if (stage == null)
                    {
                        _stateMachine.GoMainMenu(CurtainType.NoFadeOut); 
                        return;
                    }
                    
                    _stateMachine.GoJourney();
                }
            }
            catch (OperationCanceledException e)
            {
                var stateWasCancelled = e.CancellationToken == _stateCancellationTokenSource.Token;
                if (!stateWasCancelled) throw;
            }
        }

        private void FinishStage(Stage stage)
        {
            _journeyProgress.MarkStageVisited(stage.Id);
        }

        private async UniTask Play(Stage stage)
        {
            var journeyController = _journeyControllerFactory.Create(stage);
            _globalCompositeDisposable.AddDisposable(journeyController);
            
            using (var playCancellation = 
                   CancellationTokenSource.CreateLinkedTokenSource(_stateCancellationTokenSource.Token))
            {
                var playToken = playCancellation.Token;

                await journeyController.Play(playToken);
                
                playCancellation.Cancel();
            }
            
            journeyController?.Dispose();
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
        
        public void Exit()
        {
            _stateCancellationTokenSource.Dispose();
        }
    }
    
    public static partial class StateMachineExtensions
    {
        public static void GoJourney(this IStateMachine stateMachine, CurtainType? curtainType = null, AdditionalTask task = null)
            => stateMachine.EnterState<JourneyState, JourneyState.PayLoad>(new JourneyState.PayLoad
            {
                CurtainOverride = curtainType,
                AdditionalAction = task
            });
    }
}