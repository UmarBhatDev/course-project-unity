using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Features.Lootboxes.Data
{
    [CreateAssetMenu(fileName = "LootboxColorConfig", menuName = "Configs/LootboxColorConfig")]
    public class LootboxColorConfig : SerializedScriptableObject
    {
        [OdinSerialize] private Dictionary<LootboxRarenessType, Color> _colorsByLootboxRareness;

        public Color GetColorByLootboxRareness(LootboxRarenessType lootboxRarenessType)
            => _colorsByLootboxRareness.ContainsKey(lootboxRarenessType)
                ? _colorsByLootboxRareness[lootboxRarenessType]
                : Color.green;
    }
}