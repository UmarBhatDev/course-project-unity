using System;
using Base.Registries;
using Features.StoryNodes.Presenters;
using UnityEngine;

namespace Features.StoryNodes.Data
{
    [Serializable]
    public class NodeTemplate : IRegistryData
    {
        [SerializeField] public string Name;
        [SerializeField] public NodeScriptPresenter Presenter;
        public string Id => Name;

        public override string ToString()
        {
            return $"(CutsceneTemplate, Id={Id}";
        }
    }
}