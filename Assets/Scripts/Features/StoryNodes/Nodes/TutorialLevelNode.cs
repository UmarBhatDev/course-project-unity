using System.Collections;
using System.Linq;
using Features.EndPoint.Views;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;
using Unit = Unity.VisualScripting.Unit;

namespace Features.StoryNodes.Nodes
{
    [Preserve]
    public class TutorialLevelNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;

            var endPointViews = Object.FindObjectsOfType<EndPointView>(includeInactive: true).ToList();
            
            var canMoveNextNode = false;
            
            foreach (var pointView in endPointViews)
            {
                pointView.gameObject.SetActive(true);
                
                pointView.EndPointReached
                    .AsUnitObservable()
                    .First()
                    .Subscribe(_ => canMoveNextNode = true);
            }
            

            yield return new WaitUntil(() => canMoveNextNode);

            yield return Complete;
        }
    }
}