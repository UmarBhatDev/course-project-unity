using System.Collections;
using Features.Actor.Rules;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Features.StoryNodes.Nodes
{
    public class ActorSpawnNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;
        [DoNotSerialize] public ValueInput ActorSpawnTransform;

        [Inject] private ActorRule _actorRule;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            ActorSpawnTransform = ValueInput<Transform>("Actor Spawn Transform");

            Complete = ControlOutput("Complete");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;
            
            var actorSpawnTransform = flow.GetValue<Transform>(ActorSpawnTransform);

            _actorRule.Spawn(actorSpawnTransform.position, actorSpawnTransform.rotation);
            
            yield return Complete;
        }
    }
}