using Features.SceneTransitions.Factories;
using Utilities;
using Zenject;

namespace Features.SceneTransitions.Bootstrap
{
    public class CurtainInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<CurtainViewFactory>();
        }
    }
}