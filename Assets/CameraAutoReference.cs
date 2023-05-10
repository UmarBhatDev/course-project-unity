using CompassNavigatorPro;
using CoverShooter;
using Features.Actor.Rules;
using UnityEngine;
using Zenject;

public class CameraAutoReference : MonoBehaviour
{
    [Inject] 
    private ActorRule _actorRule;
    private CompassPro _compassNavigatorPro;

    [SerializeField] private GunAmmo _gunAmmo;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
         
        _compassNavigatorPro = GetComponent<CompassPro>();
        
        _compassNavigatorPro.cameraMain = Camera.current;

        if (_actorRule.GetActorView() != null)
        {
            _compassNavigatorPro.miniMapFollow = _actorRule.GetActorView().transform;
            _gunAmmo.Motor = _actorRule.GetActorView().GetComponent<CharacterMotor>();
        }
        else
        {
            _actorRule.ActorCreated += (_, view) =>
            {
                _compassNavigatorPro.miniMapFollow = view.transform;
                _gunAmmo.Motor = view.GetComponent<CharacterMotor>();
            };
        }
    }
}
