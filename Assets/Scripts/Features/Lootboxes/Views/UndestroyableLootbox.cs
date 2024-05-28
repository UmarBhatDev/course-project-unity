using CompassNavigatorPro;

namespace Features.Lootboxes.Views
{
    public class UndestroyableLootbox : BaseLootboxView
    {
        protected override void OnTaskCompleted()
        {
            Interacted?.Execute();
            
            if (IsOneTime)
            {
                InteractableStorage.RemoveItem(this);
                KeyHintCanvas.gameObject.SetActive(false);

                if (TryGetComponent<CompassProPOI>(out var poi)) poi.enabled = false;
            }
            
            CancellationTokenSource?.Cancel();
        }
    }
}