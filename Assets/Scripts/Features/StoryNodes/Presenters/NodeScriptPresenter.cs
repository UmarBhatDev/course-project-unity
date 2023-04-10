using Unity.VisualScripting;
using UnityEngine;

namespace Features.StoryNodes.Presenters
{
    public class NodeScriptPresenter : MonoBehaviour
    {
        [SerializeField] private ScriptMachine _scriptMachine;

        public ScriptMachine FlowMachine => _scriptMachine ? _scriptMachine : GetComponent<ScriptMachine>();
    }
}