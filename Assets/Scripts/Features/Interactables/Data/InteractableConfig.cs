using UnityEngine;

namespace Features.Interactables.Data
{
    [CreateAssetMenu(fileName = "InteractableViewConfig", menuName = "Configs/InteractableViewConfig")]
    public class InteractableConfig : ScriptableObject
    {
        [SerializeField] private float _interactDistance = 0.4f;

        public float InteractDistance => _interactDistance;
    }
}