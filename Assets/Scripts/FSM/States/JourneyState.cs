﻿using System;
using System.Threading;
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

        private CancellationTokenSource _stateCancellationTokenSource;

        public JourneyState(CurtainViewFactory curtainViewFactory, IStateMachine stateMachine,
            JourneyProgress journeyProgress, JourneyControllerFactory journeyControllerFactory)
        {
            _stateMachine = stateMachine;
            _journeyProgress = journeyProgress;
            _curtainViewFactory = curtainViewFactory;
            _journeyControllerFactory = journeyControllerFactory;

            _stateCancellationTokenSource = new CancellationTokenSource();
        }

        public async UniTaskVoid Enter(PayLoad payload)
        {
            _stateCancellationTokenSource = new CancellationTokenSource();

            var curtainType = payload.CurtainOverride ?? CurtainType.BlackFade;
            
            var stage = _journeyProgress.GetActiveStage();
            
            await Transition.ToScene(stage.SceneName, _curtainViewFactory, curtainType, payload.AdditionalAction);

            try
            {
                await Play(stage);
                FinishStage(stage);

                if (!_stateCancellationTokenSource.Token.IsCancellationRequested)
                {
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
            public readonly AdditionalTask AdditionalAction;
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
        public static void GoJourney(this IStateMachine stateMachine, CurtainType? curtainType = null)
            => stateMachine.EnterState<JourneyState, JourneyState.PayLoad>(new JourneyState.PayLoad
            {
                CurtainOverride = curtainType
            });
    }
}