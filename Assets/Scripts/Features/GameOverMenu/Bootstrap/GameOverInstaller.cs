using Features.GameOverMenu.Controllers;
using Features.GameOverMenu.Factories;
using Utilities;
using Zenject;

namespace Features.GameOverMenu.Bootstrap
{
    public class GameOverInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<GameOverViewFactory>();
            Container.BindFactory<GameOverController, GameOverControllerFactory>();
        }
    }
}