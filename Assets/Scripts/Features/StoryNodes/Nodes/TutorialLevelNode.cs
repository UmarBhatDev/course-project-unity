using System.Collections;
using Features.EndPoint.Factories;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Unit = Unity.VisualScripting.Unit;

namespace Features.StoryNodes.Nodes
{
    public class TutorialLevelNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;
        [DoNotSerialize] public ValueInput EndPointTransform;

        [Inject] private EndPointViewFactory _endPointViewFactory;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");
            EndPointTransform = ValueInput<Transform>("End point transform");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;
            
            var endPointTransform = flow.GetValue<Transform>(EndPointTransform);

            var endPointView = _endPointViewFactory.Create(endPointTransform);

            var canMoveNextNode = false;
            
            endPointView.EndPointReached
                .AsUnitObservable()
                .First()
                .Subscribe(_ => canMoveNextNode = true);

            yield return new WaitUntil(() => canMoveNextNode);

            yield return Complete;
        }
    }
}