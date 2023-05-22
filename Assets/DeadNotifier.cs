using CoverShooter;
using UnityEngine;

public class DeadNotifier : MonoBehaviour
{
    private CharacterMotor _characterMotor;
    private const string KillsKey = "Kills";
    
    private void Start()
    {
        _characterMotor = GetComponent<CharacterMotor>();
        
        _characterMotor.Died += () =>
        {
            var currentKillCount = PlayerPrefs.GetInt(KillsKey, 0);
            PlayerPrefs.SetInt(KillsKey, currentKillCount + 1);
        };
    }
}
