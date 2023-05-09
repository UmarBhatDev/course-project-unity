using System.Collections.Generic;
using UnityEngine;

namespace Features.Hints.Data
{
    [CreateAssetMenu(fileName = "KeyGraphicsRegistry", menuName = "Registries/KeyGraphicsRegistry")]
    public class KeyGraphicsRegistry : ScriptableObject
    {
        [SerializeField] private List<Sprite> _tappedKeySprites;
        [SerializeField] private List<Sprite> _untappedKeySprites;

        public List<Sprite> TappedKeySprites => _tappedKeySprites;
        public List<Sprite> UntappedKeySprites => _untappedKeySprites;
    }
}