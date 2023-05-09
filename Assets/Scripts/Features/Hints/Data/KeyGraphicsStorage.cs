using System.Collections.Generic;
using Base.Registries;
using UnityEngine;
using Zenject;

namespace Features.Hints.Data
{
    public class KeyGraphicsStorage : IInitializable
    {
        private readonly KeyGraphicsRegistry _keyGraphicsRegistry;
        private readonly Dictionary<string, Sprite> _tappedKeySprites;
        private readonly Dictionary<string, Sprite> _untappedKeySprites;

        public KeyGraphicsStorage(KeyGraphicsRegistry keyGraphicsRegistry)
        {
            _keyGraphicsRegistry = keyGraphicsRegistry;
            _tappedKeySprites = new Dictionary<string, Sprite>();
            _untappedKeySprites = new Dictionary<string, Sprite>();
        }

        public void Initialize()
        {
            foreach (var keySprite in _keyGraphicsRegistry.TappedKeySprites)
                _tappedKeySprites.Add(keySprite.name, keySprite);
            foreach (var keySprite in _keyGraphicsRegistry.UntappedKeySprites)
                _untappedKeySprites.Add(keySprite.name, keySprite);
        }

        public Sprite GetUntappedKeySprite(KeyCode keyCode)
        {
            var keyName = keyCode.ToString();

            return _tappedKeySprites.ContainsKey(keyName) ? _tappedKeySprites[keyName] : null;
        }
        
        public Sprite GetTappedKeySprite(KeyCode keyCode)
        {
            var keyName = keyCode.ToString();

            return _untappedKeySprites.ContainsKey(keyName) ? _untappedKeySprites[keyName] : null;
        }
    }
}