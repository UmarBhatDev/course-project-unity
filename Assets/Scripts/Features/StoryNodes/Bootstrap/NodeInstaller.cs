using Features.StoryNodes.Data;
using Features.StoryNodes.Factories;
using Features.StoryNodes.Services;
using Utilities;
using Zenject;

namespace Features.StoryNodes.Bootstrap
{
    public class NodeInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindFactory<string, NodeScript, NodeScriptPlaceholderFactory>()
                .FromFactory<NodeScriptFactory>();
                
            Container.InstallService<NodeService>();
        }
    }
}