using Features.Hints.Data;
using Features.Hints.Services;
using Utilities;
using Zenject;

namespace Features.Hints.Installers
{
    public class KeyInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallModels();
            InstallServices();
        }

        private void InstallServices()
        {
            Container.InstallService<KeyCodeService>();
        }

        private void InstallModels()
        {
            Container.InstallModel<KeyGraphicsStorage>();
        }
    }
}