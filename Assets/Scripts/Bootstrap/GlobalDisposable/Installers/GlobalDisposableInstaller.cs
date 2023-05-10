using Bootstrap.GlobalDisposable.Services;
using Utilities;
using Zenject;

namespace Bootstrap.GlobalDisposable.Installers
{
    public class GlobalDisposableInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<GlobalCompositeDisposable>();
        }
    }
}