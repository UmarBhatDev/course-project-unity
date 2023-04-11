using Features.Actor.Factories;
using Features.Actor.Rules;
using Utilities;
using Zenject;

namespace Features.Actor.Installers
{
    public class ActorInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallGameRule<ActorRule>();
            Container.InstallFactory<ActorViewFactory>();
            Container.InstallFactory<ActorModelFactory>();
            Container.InstallFactory<ActorControllerFactory>();
        }
    }
}