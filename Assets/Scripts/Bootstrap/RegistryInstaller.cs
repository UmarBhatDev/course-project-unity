using Bootstrap.CanvasBootstrap.Data;
using Features.Hints.Data;
using Features.Interactables.Data;
using Features.Lootboxes.Data;
using Features.Roadmap.Data;
using Features.SceneTransitions.Data;
using Features.StoryNodes.Data;
using UnityEngine;
using Utilities;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(fileName = "RegistryInstaller", menuName = "Installers/RegistryInstaller")]
    public class RegistryInstaller : ScriptableObjectInstaller<RegistryInstaller>
    {
        [Header("Game")]
        [SerializeField] private DefaultKeyBindsRegistry _defaultKeyBindsRegistry;
        [SerializeField] private NodeTemplateRegistry _nodeTemplateRegistry;
        [SerializeField] private KeyGraphicsRegistry _keyGraphicsRegistry;
        [SerializeField] private LootboxColorConfig _lootboxColorConfig;
        [SerializeField] private InteractableConfig _interactableConfig;
        [SerializeField] private CurtainRegistry _curtainRegistry;
        [SerializeField] private RoadmapRegistry _roadmapRegistry;
        [SerializeField] private ViewRegistry _viewData;

        public override void InstallBindings()
        {
            Container.InstallRegistry(_defaultKeyBindsRegistry);
            Container.InstallRegistry(_nodeTemplateRegistry);
            Container.InstallRegistry(_keyGraphicsRegistry);
            Container.InstallRegistry(_lootboxColorConfig);
            Container.InstallRegistry(_interactableConfig);
            Container.InstallRegistry(_curtainRegistry);
            Container.InstallRegistry(_roadmapRegistry);
            Container.InstallRegistry(_viewData);
        }
    }
}