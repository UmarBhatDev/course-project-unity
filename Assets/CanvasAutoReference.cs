using CompassNavigatorPro;
using CoverShooter;
using Features.Actor.Rules;
using UnityEngine;
using Zenject;

public class CanvasAutoReference : MonoBehaviour
{
    [Inject] 
    private ActorRule _actorRule;
    private CompassPro _compassNavigatorPro;

    [SerializeField] private GunAmmo _gunAmmo;
    [SerializeField] private GameObject _compass;
    [SerializeField] private GameObject _miniMap;
    [SerializeField] private HealthBar _playerHealth;

    public GunAmmo GunAmmo => _gunAmmo;
    public GameObject Compass => _compass;
    public GameObject MiniMap => _miniMap;
    public HealthBar PlayerHealth => _playerHealth;
    public CompassPro CompassNavigatorPro => _compassNavigatorPro ? _compassNavigatorPro : GetComponent<CompassPro>();

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
            var actorView = _actorRule.GetActorView();
            _compassNavigatorPro.miniMapFollow = actorView.transform;
            _gunAmmo.Motor = actorView.GetComponent<CharacterMotor>();
            _playerHealth.Target = actorView.gameObject;
        }
        else
        {
            _actorRule.ActorCreated += (_, view) =>
            {
                _compassNavigatorPro.miniMapFollow = view.transform;
                _gunAmmo.Motor = view.GetComponent<CharacterMotor>();
                _playerHealth.Target = view.gameObject;
            };
        }
    }
}
