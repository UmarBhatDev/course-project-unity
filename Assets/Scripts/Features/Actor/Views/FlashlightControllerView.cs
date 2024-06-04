using Bootstrap;
using CoverShooter;
using UnityEngine;

namespace Features.Actor.Views
{
    public class FlashlightControllerView : MonoBehaviour
    {
        [SerializeField] private CharacterMotor _characterMotor;
        [SerializeField] private Light _light;
        [SerializeField] private bool _flashOnZoom;

        private bool _canUseOnThisLevel;

        private void Start()
        {
            var levelSettings = FindObjectOfType<LevelSettings>();
            _canUseOnThisLevel = levelSettings != null && levelSettings.ShouldUseFlashLight;
        }

        private void Update()
        {
            _light.enabled = _canUseOnThisLevel && (_characterMotor.IsZooming ? _flashOnZoom : !_flashOnZoom);
        }
    }
}