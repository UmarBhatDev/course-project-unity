using UnityEngine;

namespace Bootstrap
{
    public class LevelSettings : MonoBehaviour
    {
        public bool ShouldUseFlashLight => _shouldUseFlashLight;
        
        [SerializeField] private bool _shouldUseFlashLight;
    }
}