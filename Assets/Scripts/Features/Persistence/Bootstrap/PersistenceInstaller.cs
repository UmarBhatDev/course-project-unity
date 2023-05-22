using Features.Persistence.Services;
using Utilities;
using Zenject;

namespace Features.Persistence.Bootstrap
{
    public class PersistenceInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<JourneyProgress>();
            Container.InstallService<InventoryProgress>();
        }
    }
}