using Features.Hints.Data;
using ModestTree;
using UnityEngine;

namespace Features.Hints.Services
{
    public class KeyCodeService
    {
        private readonly DefaultKeyBindsRegistry _defaultKeyBindsRegistry;

        public KeyCodeService(DefaultKeyBindsRegistry defaultKeyBindsRegistry)
        {
            _defaultKeyBindsRegistry = defaultKeyBindsRegistry;
        }

        public void SaveKeyBind(KeyType keyType, KeyCode keyCode)
        {
            PlayerPrefs.SetString(keyType.ToString(), JsonUtility.ToJson(keyCode));
        }

        public KeyCode GetKeyBind(KeyType keyType)
        {
            var defaultKeyCode = _defaultKeyBindsRegistry.GetKeyCodeForAction(keyType);
            var defaultKeySerialized = JsonUtility.ToJson(defaultKeyCode);
            var keySerialized = PlayerPrefs.GetString(keyType.ToString(), defaultKeySerialized);
            var keyDeserialized = keySerialized.IsEmpty()
                ? JsonUtility.FromJson<KeyCode>(defaultKeySerialized)
                : JsonUtility.FromJson<KeyCode>(keySerialized);

            return keyDeserialized;
        }
    }
}