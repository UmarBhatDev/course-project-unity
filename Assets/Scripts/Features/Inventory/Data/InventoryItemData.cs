using Features.Lootboxes.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Inventory.Data
{
    public struct InventoryItemData
    {
        public InventoryItemType ItemType;
        public LootboxRarenessType ItemRareness;
        public Sprite ItemIcon;
    }
}