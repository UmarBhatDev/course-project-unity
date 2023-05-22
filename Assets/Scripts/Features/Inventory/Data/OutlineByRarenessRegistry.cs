using System.Collections.Generic;
using Features.Lootboxes.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Inventory.Data
{
    [CreateAssetMenu(fileName = "OutlineByRarenessRegistry", menuName = "Registries/OutlineByRarenessRegistry")]
    public class OutlineByRarenessRegistry : SerializedScriptableObject
    {
        public Dictionary<LootboxRarenessType, Sprite> OutLineByRareness;

        public Sprite GetOutlineByRareness(LootboxRarenessType rarenessType)
        {
            return OutLineByRareness.ContainsKey(rarenessType) 
                ? OutLineByRareness[rarenessType] 
                : OutLineByRareness[LootboxRarenessType.Common];
        }
    }
}