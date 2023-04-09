using Features.Journey.Controllers;
using Features.Journey.Factories;
using Features.Roadmap.Data;
using Zenject;

namespace Features.Journey.Bootstrap
{
    public class JourneyInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container
                .BindFactory<Stage, JourneyController, JourneyControllerFactory>()
                .AsSingle();
        }
    }
}