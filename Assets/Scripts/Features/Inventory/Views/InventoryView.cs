using System.Collections.Generic;
using System.Linq;
using Features.Inventory.Data;
using Features.Inventory.Model;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Features.Inventory.Views
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private List<SlotView> _allSlotViews;

        private InventoryStorage _inventoryStorage;
        private InventoryRegistry _inventoryRegistry;

        [Inject]
        public void Construct(InventoryRegistry inventoryRegistry, InventoryStorage inventoryStorage)
        {
            _inventoryRegistry = inventoryRegistry;
            _inventoryStorage = inventoryStorage;
        }

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Start()
        {
            foreach (var slotView in _allSlotViews)
            {
                var itemData = _inventoryRegistry.GetIconForItemId(InventoryItemType.None);

                slotView.SetItem(itemData, 0, isBusy: false);
            }
            
            var claimedItemIds = _inventoryStorage.GetAllClaimedItemIds();

            foreach (var inventoryItem in claimedItemIds)
            {
                var itemData = _inventoryRegistry.GetIconForItemId(inventoryItem.Key);
                
                var randomView = FindRandomSlot();
                if (randomView == null) break;

                randomView.SetItem(itemData, inventoryItem.Value);
            }

            _inventoryStorage.ItemClaimed += AddItemToSlot;
            _inventoryStorage.ItemLost += RemoveItemFromSlot;
            
            foreach (var slotView in _allSlotViews)
            {
                slotView.SlotSelected += view =>
                {
                    foreach (var unselectedSlotView in _allSlotViews.Where(unselectedSlotView =>
                                 unselectedSlotView != view))
                        unselectedSlotView.SetOutlineActive(false);
                };
            }
        }

        private void AddItemToSlot(InventoryItemType itemType, int count)
        {
            var areThereExistingSlots = _allSlotViews.Any(x => x.InventoryItemType == itemType);

            if (areThereExistingSlots)
            {
                var existingSlot = _allSlotViews.First(x => x.InventoryItemType == itemType);
                existingSlot.SetCount(count);
            }
            else
            {
                var randomView = FindRandomSlot();
                if (randomView == null) return;
                    
                var itemData = _inventoryRegistry.GetIconForItemId(itemType);
                randomView.SetItem(itemData, count);
            }
        }

        private void RemoveItemFromSlot(InventoryItemType itemType, int count)
        {
            var areThereExistingSlots = _allSlotViews.Any(x => x.InventoryItemType == itemType);

            if (!areThereExistingSlots) return;
            
            var existingSlot = _allSlotViews.First(x => x.InventoryItemType == itemType);
            existingSlot.SetCount(count);
        }

        private SlotView FindRandomSlot()
        {
            var areThereFreeSlots = _allSlotViews.Any(x => !x.IsBusy);
            if (!areThereFreeSlots) return null;
            
            SlotView randomView;

            do
            {
                var random = new Random();
                var randomIndex = random.Next(0, _allSlotViews.Count);

                randomView = _allSlotViews[randomIndex];

            } while (randomView.IsBusy);

            return randomView;
        }
    }
}