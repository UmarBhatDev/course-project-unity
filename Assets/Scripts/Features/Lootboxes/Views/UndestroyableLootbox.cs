namespace Features.Lootboxes.Views
{
    public class UndestroyableLootbox : BaseLootboxView
    {
        protected override void OnTaskCompleted()
        {
            Interacted?.Execute();
        }
    }
}