using System;
using System.Linq;
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
using UnityEngine.Scripting;

namespace FSM.States
{
    [Preserve]
    public class JourneyState : IGameState<JourneyState.PayLoad>
    {
        private readonly IStateMachine _stateMachine;
        private readonly JourneyProgress _journeyProgress;
        private readonly RoadmapRegistry _roadmapRegistry;
        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly JourneyControllerFactory _journeyControllerFactory;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;

        private CancellationTokenSource _stateCancellationTokenSource;

        public JourneyState(CurtainViewFactory curtainViewFactory, IStateMachine stateMachine,
            JourneyProgress journeyProgress, JourneyControllerFactory journeyControllerFactory,
            GlobalCompositeDisposable globalCompositeDisposable, RoadmapRegistry roadmapRegistry)
        {
            _stateMachine = stateMachine;
            _journeyProgress = journeyProgress;
            _roadmapRegistry = roadmapRegistry;
            _curtainViewFactory = curtainViewFactory;
            _journeyControllerFactory = journeyControllerFactory;
            _globalCompositeDisposable = globalCompositeDisposable;

            _stateCancellationTokenSource = new CancellationTokenSource();
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            _stateCancellationTokenSource = new CancellationTokenSource();

            var curtainType = payload.CurtainOverride ?? CurtainType.BlackFade;

            var overrideStage = payload.OverrideActiveStage ?? string.Empty;
            var isPresentInRoadmap = _roadmapRegistry.Roadmap.Stages.Any(x => x.Id == overrideStage);

            var stage = overrideStage != string.Empty && isPresentInRoadmap
                ? _roadmapRegistry.Roadmap.Stages.First(x => x.Id == overrideStage)
                : _journeyProgress.GetActiveStage();

            if (stage == null)
            {
                _stateMachine.GoMainMenu(CurtainType.BlackFade); 
                return;
            }
            
            await Transition.ToScene(stage.SceneName, _curtainViewFactory, _globalCompositeDisposable, curtainType, payload.AdditionalAction);

            try
            {
                await Play(stage);

                if (!_stateCancellationTokenSource.Token.IsCancellationRequested)
                {
                    _journeyProgress.MarkStageVisited(stage.Id);

                    stage = _journeyProgress.GetActiveStage();

                    if (stage == null)
                    {
                        _stateMachine.GoMainMenu(CurtainType.BlackFade); 
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
            
            journeyController.Dispose();
        }
        
        public struct PayLoad
        {
            public string OverrideActiveStage;
            public AdditionalTask AdditionalAction;
            public CurtainType? CurtainOverride { get; set; }
        }
        
        public void Exit()
        {
            _stateCancellationTokenSource.Dispose();
        }
    }
    
    public static partial class StateMachineExtensions
    {
        public static void GoJourney(this IStateMachine stateMachine, string overrideActiveStage = null, CurtainType? curtainType = null, AdditionalTask task = null)
            => stateMachine.EnterState<JourneyState, JourneyState.PayLoad>(new JourneyState.PayLoad
            {
                OverrideActiveStage = overrideActiveStage,
                CurtainOverride = curtainType,
                AdditionalAction = task
            });
    }
}