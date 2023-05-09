using Features.Lootboxes.Services;
using Utilities;
using Zenject;

namespace Features.Lootboxes.Bootstrap
{
    public class LootboxInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallServices();
        }

        private void InstallServices()
        {
            Container.InstallService<LootboxService>();
        }
    }
}