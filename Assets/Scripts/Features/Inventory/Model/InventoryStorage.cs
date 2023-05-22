using System;
using System.Collections.Generic;
using Features.Inventory.Data;
using Features.Persistence.Services;
using Unity.VisualScripting;

namespace Features.Inventory.Model
{
    public class InventoryStorage : IInitializable
    {
        public event Action<InventoryItemType, int> ItemLost;
        public event Action<InventoryItemType, int> ItemClaimed; 
        
        private Dictionary<InventoryItemType, int> _claimedItems;

        private readonly InventoryProgress _inventoryProgress;

        public InventoryStorage(InventoryProgress inventoryProgress)
        {
            _claimedItems = new Dictionary<InventoryItemType, int>();
            _inventoryProgress = inventoryProgress;
        }

        public void Initialize()
        {
            var claimedItemTypes = _inventoryProgress.GetCurrentClaimedItems();
            
            foreach (var inventoryItemType in claimedItemTypes)
            {
                _claimedItems.Add(inventoryItemType.Key, inventoryItemType.Value);
            }
        }

        public Dictionary<InventoryItemType, int> GetAllClaimedItemIds()
            => _claimedItems;

        public void AddClaimedItemId(InventoryItemType type)
        {
            if (_claimedItems.ContainsKey(type))
                _claimedItems[type]++;
            else 
                _claimedItems.Add(type, 1);

            _inventoryProgress.SaveCurrentClaimedItems(_claimedItems);
            
            ItemClaimed?.Invoke(type, _claimedItems[type]);
        }

        public void RemoveClaimedItemId(InventoryItemType type)
        {
            if (_claimedItems.ContainsKey(type) && _claimedItems[type] > 1)
                _claimedItems[type]--;
            else 
                _claimedItems.Remove(type);
            
            _inventoryProgress.SaveCurrentClaimedItems(_claimedItems);
            
            ItemLost?.Invoke(type, _claimedItems[type]);
        }
    }
}