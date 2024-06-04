using UnityEngine;

namespace Features.Roadmap.Data
{
    [CreateAssetMenu]
    public class RoadmapRegistry : ScriptableObject
    {
        public Roadmap Roadmap => _roadmap;
        public Sprite UnknownLocationSprite => _unknownLocationSprite;

        [SerializeField] private Roadmap _roadmap;
        [SerializeField] private Sprite _unknownLocationSprite;
    }
}