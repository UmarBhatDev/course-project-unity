using Features.StoryNodes.Tutorials.Factories;
using Utilities;
using Zenject;

namespace Features.StoryNodes.Tutorials.Installers
{
    public class TutorialInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallFactories();
        }

        private void InstallFactories()
        {
            Container.InstallFactory<TutorialViewFactory>();
        }
    }
}