using Features.EndPoint.Factories;
using Utilities;
using Zenject;

namespace Features.EndPoint.Installers
{
    public class EndPointInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<EndPointViewFactory>();
        }
    }
}

