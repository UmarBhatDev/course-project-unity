using Features.Hints.Data;
using ModestTree;
using Newtonsoft.Json;
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
            PlayerPrefs.SetString(keyType.ToString(), JsonConvert.SerializeObject(keyCode));
        }

        public KeyCode GetKeyBind(KeyType keyType)
        {
            var defaultKeyCode = _defaultKeyBindsRegistry.GetKeyCodeForAction(keyType);
            var defaultKeySerialized = JsonConvert.SerializeObject(defaultKeyCode);
            var keySerialized = PlayerPrefs.GetString(keyType.ToString(), defaultKeySerialized);
            var keyDeserialized = keySerialized.IsEmpty()
                ? JsonConvert.DeserializeObject<KeyCode>(defaultKeySerialized)
                : JsonConvert.DeserializeObject<KeyCode>(keySerialized);

            return keyDeserialized;
        }
    }
}