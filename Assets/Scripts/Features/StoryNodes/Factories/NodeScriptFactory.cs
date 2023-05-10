using System.Linq;
using Features.StoryNodes.Data;
using Features.StoryNodes.Presenters;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Features.StoryNodes.Factories
{
    public class NodeScriptPlaceholderFactory : PlaceholderFactory<string, NodeScriptPresenter, NodeScript>
    {
    }

    public class NodeScriptFactory : IFactory<string, NodeScriptPresenter, NodeScript>
    {
        private readonly DiContainer _diContainer;
        private readonly NodeTemplateRegistry _cutsceneTemplateRegistry;

        public NodeScriptFactory(DiContainer diContainer, NodeTemplateRegistry cutsceneTemplateRegistry)
        {
            _cutsceneTemplateRegistry = cutsceneTemplateRegistry;
            _diContainer = diContainer;
        }

        public NodeScript Create(string techName, NodeScriptPresenter nodeScriptPresenter)
        {
            if (nodeScriptPresenter == null)
                return null;
            
            var go = Object.Instantiate(nodeScriptPresenter.gameObject);
            
            var view = go.GetComponent<NodeScriptPresenter>();
            
            var script = new NodeScript(view, techName);
            
            InjectDependencies(view.FlowMachine);

            return script;
        }

        private void InjectDependencies(ScriptMachine scriptMachine)
        {
            var units = scriptMachine.nest?.graph?.units?.ToArray();
            
            if (units == null) 
                return;
            foreach (var unit in units)
                _diContainer.Inject(unit);
        }
    }
}