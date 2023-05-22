using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = System.Random;

namespace Features.Inventory.Data
{
    [CreateAssetMenu(fileName = "InventoryRegistry", menuName = "Registries/InventoryRegistry")]
    public class InventoryRegistry : SerializedScriptableObject
    {
        public List<InventoryItemData> ItemsData;
        
        public InventoryItemData GetIconForItemId(InventoryItemType itemType)
        {
            return ItemsData.Any(x => x.ItemType == itemType) 
                ? ItemsData.First(x => x.ItemType == itemType) 
                : GetRandomImage();
        }

        private InventoryItemData GetRandomImage()
        {
            var images = ItemsData;
            
            var random = new Random();
            var randomIndex = random.Next(0, images.Count);

            return images[randomIndex];
        }
    } 
}