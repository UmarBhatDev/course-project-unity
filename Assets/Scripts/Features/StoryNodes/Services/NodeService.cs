using System.Collections.Generic;
using Features.StoryNodes.Data;
using Features.StoryNodes.Factories;

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

        public NodeScript CreateScript(string techName)
        {
            var script = _nodeTemplateRegistry.Create(techName);
            _scripts.Add(script);

            return script;
        }
    }
}