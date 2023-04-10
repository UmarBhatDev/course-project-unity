using System;
using Base.Registries;
using UnityEngine;

namespace Features.StoryNodes.Data
{
    [Serializable]
    [CreateAssetMenu(fileName = "NodeTemplateRegistry", menuName = "Registries/NodeTemplateRegistry")]
    public class NodeTemplateRegistry : RegistryListBase<NodeTemplate>
    {
        public NodeTemplate FindByTechName(string techName)
        {
            return GetById(techName);
        }
    }
}