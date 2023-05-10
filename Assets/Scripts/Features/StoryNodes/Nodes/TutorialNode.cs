using System.Collections;
using Features.StoryNodes.Tutorials.Factories;
using Features.StoryNodes.Tutorials.Views;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Unit = Unity.VisualScripting.Unit;

namespace Features.StoryNodes.Nodes
{
    public class TutorialNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;
        [DoNotSerialize] public ValueInput TutorialView;

        [Inject] private TutorialViewFactory _tutorialNodeFactory;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");
            TutorialView = ValueInput<GameObject>("TutorialHandler");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;

            var tutorialObject = flow
                .GetValue<GameObject>(TutorialView)
                .GetComponent<TutorialViewBase>();
            
            var tutorialView = _tutorialNodeFactory.Create(tutorialObject);

            var canMoveNextNode = false;

            tutorialView.TutorialPassed
                .AsUnitObservable()
                .First()
                .Subscribe(_ => canMoveNextNode = true);

            yield return new WaitUntil(() => canMoveNextNode);

            yield return Complete;
        }
    }
}