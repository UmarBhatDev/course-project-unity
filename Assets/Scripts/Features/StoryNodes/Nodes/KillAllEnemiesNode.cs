using System.Collections;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Unit = Unity.VisualScripting.Unit;

namespace Features.StoryNodes.Nodes
{
    public class KillAllEnemiesNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;
        [DoNotSerialize] public ValueInput EnemiesCount;
        
        private const string KillsKey = "Kills";

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");
            EnemiesCount = ValueInput<int>("Enemies Count");
        }
        
        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;
            
            var enemiesCount = flow.GetValue<int>(EnemiesCount);

            var canMoveNextNode = false;

            var disposable = Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    if (PlayerPrefs.GetInt(KillsKey, 0) >= enemiesCount)
                        canMoveNextNode = true;
                });
            
            yield return new WaitUntil(() => canMoveNextNode);
            
            disposable?.Dispose();

            yield return Complete;
        }
    }
}