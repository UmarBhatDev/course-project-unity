using Features.Lootboxes.Data;
using UnityEngine;

namespace Features.Lootboxes.Services
{
    public class LootboxService
    {
        private readonly LootboxColorConfig _lootboxColorConfig;

        public LootboxService(LootboxColorConfig lootboxColorConfig)
        {
            _lootboxColorConfig = lootboxColorConfig;
        }

        public Color GetLootColor(LootboxRarenessType lootboxRarenessType)
            => _lootboxColorConfig.GetColorByLootboxRareness(lootboxRarenessType);
    }
}