using Features.Inventory.Controllers;
using Features.Inventory.Factories;
using Features.Inventory.Model;
using Features.Inventory.Services;
using Utilities;
using Zenject;

namespace Features.Inventory.Bootstrap
{
    public class InventoryInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallModel<InventoryStorage>();
            Container.InstallService<InventoryService>();
            Container.InstallFactory<InventoryViewFactory>();
            
            Container.BindFactory<InventoryController, InventoryControllerFactory>();
        }
    }
}