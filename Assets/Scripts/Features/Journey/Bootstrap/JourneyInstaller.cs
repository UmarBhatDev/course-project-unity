using Features.Journey.Factories;
using Utilities;
using Zenject;

namespace Features.Journey.Bootstrap
{
    public class JourneyInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<JourneyControllerFactory>();
        }
    }
}