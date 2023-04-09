using Bootstrap.CanvasBootstrap.Data;
using Features.Roadmap.Data;
using Features.SceneTransitions.Data;
using UnityEngine;
using Utilities;
using Zenject;

namespace Bootstrap
{
    [CreateAssetMenu(fileName = "RegistryInstaller", menuName = "Installers/RegistryInstaller")]
    public class RegistryInstaller : ScriptableObjectInstaller<RegistryInstaller>
    {
        [Header("Game")]
        [SerializeField] private CurtainRegistry _curtainRegistry;
        [SerializeField] private RoadmapRegistry _roadmapRegistry;
        [SerializeField] private ViewRegistry viewData;

        public override void InstallBindings()
        {
            Container.InstallRegistry(_curtainRegistry);
            Container.InstallRegistry(_roadmapRegistry);
            Container.InstallRegistry(viewData);
        }
    }
}