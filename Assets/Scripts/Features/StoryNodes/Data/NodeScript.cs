using Features.StoryNodes.Presenters;
using ModestTree;
using UnityEngine;

namespace Features.StoryNodes.Data
{
    public class NodeScript
    {
        private readonly NodeScriptPresenter _presenter;
        private readonly string _techName;

        public NodeScript(NodeScriptPresenter presenter, string techName)
        {
            _presenter = presenter;
            _techName = techName;
        }

        public void Play()
        {
        }
    }
}