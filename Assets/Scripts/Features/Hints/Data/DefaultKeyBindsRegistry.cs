using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Hints.Data
{
    [CreateAssetMenu(fileName = "DefaultKeyBindsRegistry", menuName = "Registries/DefaultKeyBindsRegistry")]
    public class DefaultKeyBindsRegistry : SerializedScriptableObject
    {
        public Dictionary<KeyType, KeyCode> KeyBinds;

        public KeyCode GetKeyCodeForAction(KeyType keyType)
        {
            return KeyBinds.First(x => x.Key == keyType).Value;
        }
    }
}