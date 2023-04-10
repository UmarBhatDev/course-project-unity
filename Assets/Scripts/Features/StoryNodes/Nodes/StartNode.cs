using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Features.StoryNodes.Nodes
{
    public class StartNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;

        [DoNotSerialize] public ValueInput ScriptName;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");

            ScriptName = ValueInput("Script Name", "");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            var script = flow.GetValue<string>(ScriptName);

            Debug.Log("WORKED");
            // _signalBus.Fire(new CutsceneSignals.StartScript(script));
            yield return Complete;
        }
    }
}