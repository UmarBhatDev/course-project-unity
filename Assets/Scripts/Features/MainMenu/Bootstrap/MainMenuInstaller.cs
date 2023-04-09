using Features.MainMenu.Controllers;
using Features.MainMenu.Factories;
using Utilities;
using Zenject;

namespace Features.MainMenu.Bootstrap
{
    public class MainMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<MainMenuViewFactory>();
            Container.BindFactory<MainMenuController, MainMenuControllerFactory>();
        }
    }
}