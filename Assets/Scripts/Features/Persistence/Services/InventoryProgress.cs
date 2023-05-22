using System.Collections.Generic;
using Features.Inventory.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace Features.Persistence.Services
{
    public class InventoryProgress
    {
        private const string _claimedItemsPrefsKey = "ClaimedItems";

        public Dictionary<InventoryItemType, int> GetCurrentClaimedItems()
        {
            var itemsJson = PlayerPrefs.GetString(_claimedItemsPrefsKey, string.Empty);

            if (itemsJson == string.Empty)
                return new Dictionary<InventoryItemType, int>();

            var items = JsonConvert.DeserializeObject<Dictionary<InventoryItemType, int>>(itemsJson);

            return items;
        }
        
        public void SaveCurrentClaimedItems(Dictionary<InventoryItemType, int> itemIds)
        {
            var itemsJson = JsonConvert.SerializeObject(itemIds);

            PlayerPrefs.SetString(_claimedItemsPrefsKey, itemsJson);
        }
    }
}