using System.Collections;
using Features.Persistence.Services;
using FSM;
using FSM.Data;
using FSM.States;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Unit = Unity.VisualScripting.Unit;

namespace Features.StoryNodes.Nodes
{
    public class LevelPassedNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;

        [Inject] private JourneyProgress _journeyProgress;
        [Inject] private IStateMachine _stateMachine;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;

            yield return new WaitForSeconds(3);

            var activeStage = _journeyProgress.GetActiveStage();
            _journeyProgress.MarkStageVisited(activeStage.Id);

            _stateMachine.GoSavePointState(CurtainType.BlackFade);
            
            yield return Complete;
        }

    }
}