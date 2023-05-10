using System.Collections.Generic;
using Features.StoryNodes.Data;
using Features.StoryNodes.Factories;
using Features.StoryNodes.Presenters;

namespace Features.StoryNodes.Services
{
    public class NodeService
    {
        private readonly List<NodeScript> _scripts;
        private readonly NodeScriptPlaceholderFactory _nodeTemplateRegistry;

        public NodeService(NodeScriptPlaceholderFactory nodeTemplateRegistry)
        {
            _nodeTemplateRegistry = nodeTemplateRegistry;
            _scripts = new List<NodeScript>();
        }

        public NodeScript CreateScript(string techName, NodeScriptPresenter prefab)
        {
            var script = _nodeTemplateRegistry.Create(techName, prefab);
            _scripts.Add(script);

            return script;
        }
    }
}