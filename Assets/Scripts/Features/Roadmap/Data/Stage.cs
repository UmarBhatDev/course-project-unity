using System;
using Features.StoryNodes.Presenters;
using UnityEngine;

namespace Features.Roadmap.Data
{
    [Serializable]
    public class Stage
    {
        public string Id;
        public string SceneName;
        public Sprite LocationPreviewSprite;
        public NodeScriptPresenter Script;
        public NodeScriptPresenter LevelPassedScript;
    }
}