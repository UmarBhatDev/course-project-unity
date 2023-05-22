using CompassNavigatorPro;
using Features.Inventory.Data;
using Features.Inventory.Model;
using UnityEngine;
using Zenject;

namespace Features.Lootboxes.Views
{
    public class OpenableLootbox : BaseLootboxView
    {
        [SerializeField] private GameObject _openedLootbox;
        [SerializeField] private GameObject _closedLootbox;

        [Inject] private InventoryStorage _inventoryStorage;
        
        protected override void OnTaskCompleted()
        {
            _inventoryStorage.AddClaimedItemId(InventoryItemType.AkEpic);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.AkRare);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.AmmoCommon);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.AmmoCommon);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.AmmoCommon);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.MedkitRare);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.TapeCommon);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.SniperEpic);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.SniperEpic);
            _inventoryStorage.AddClaimedItemId(InventoryItemType.VectorLegendary);
            
            _closedLootbox.SetActive(false);
            _openedLootbox.SetActive(true);
            
            if (IsOneTime && TryGetComponent<CompassProPOI>(out var poi))
                poi.enabled = false;
            
            Interacted?.Execute();
            CancellationTokenSource?.Cancel();
            InteractableStorage.RemoveItem(this);
        }
    }
}