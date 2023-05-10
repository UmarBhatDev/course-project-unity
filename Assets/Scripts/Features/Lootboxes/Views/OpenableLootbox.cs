using CompassNavigatorPro;
using UnityEngine;

namespace Features.Lootboxes.Views
{
    public class OpenableLootbox : BaseLootboxView
    {
        [SerializeField] private GameObject _openedLootbox;
        [SerializeField] private GameObject _closedLootbox;
        
        protected override void OnTaskCompleted()
        {
            _closedLootbox.SetActive(false);
            _openedLootbox.SetActive(true);
            
            if (IsOneTime && TryGetComponent<CompassProPOI>(out var poi))
                poi.enabled = false;
            
            CancellationTokenSource?.Cancel();
            InteractableStorage.RemoveItem(this);
        }
    }
}