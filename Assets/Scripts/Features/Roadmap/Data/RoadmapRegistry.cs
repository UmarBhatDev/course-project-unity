using UnityEngine;

namespace Features.Roadmap.Data
{
    [CreateAssetMenu]
    public class RoadmapRegistry : ScriptableObject
    {
        public Roadmap Roadmap => _roadmap;

        [SerializeField] private Roadmap _roadmap;
    }
}