using Features.SavePointMenu.Controllers;
using Features.SavePointMenu.Factories;
using UnityEngine.Scripting;
using Utilities;
using Zenject;

namespace Features.SavePointMenu.Bootstrap
{
    [Preserve]
    public class SavePointMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<SavePointMenuViewFactory>();
            Container.BindFactory<SavePointMenuController, SavePointMenuControllerFactory>();
        }
    }
}