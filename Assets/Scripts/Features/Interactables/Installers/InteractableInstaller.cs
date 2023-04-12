using Features.Interactables.Data;
using Features.Interactables.Factories;
using Features.Interactables.Services;
using Utilities;
using Zenject;

namespace Features.Interactables.Installers
{
    public class InteractableInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallModel<InteractableStorage>();
            Container.InstallGameRule<InteractableRule>();
            Container.InstallFactory<InteractableViewFactory>();
        }
    }
}