using Features.Inventory.Factories;
using Features.Inventory.Views;
using UnityEngine;

namespace Features.Inventory.Controllers
{
    public class InventoryController
    {
        private readonly InventoryViewFactory _inventoryViewFactory;

        private readonly InventoryView _inventoryView;
        
        public InventoryController(InventoryViewFactory inventoryViewFactory)
        {
            _inventoryViewFactory = inventoryViewFactory;
            
            _inventoryView = _inventoryViewFactory.Create();
        }

        public void ToggleInventory()
        {
            var isActive = _inventoryView.gameObject.activeSelf;
            
            if (isActive)
            {
                Time.timeScale = 1f;
                
                _inventoryView.gameObject.SetActive(false);
                return;
            }
            
            Time.timeScale = 0f;
            
            _inventoryView.gameObject.SetActive(true);
        }
    }
}