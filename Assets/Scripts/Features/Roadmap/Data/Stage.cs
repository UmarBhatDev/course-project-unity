using System;
using Features.StoryNodes.Presenters;

namespace Features.Roadmap.Data
{
    [Serializable]
    public class Stage
    {
        public string Id;
        public string SceneName;
        public NodeScriptPresenter Script;
    }
}