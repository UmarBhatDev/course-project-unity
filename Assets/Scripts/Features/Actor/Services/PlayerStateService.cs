using System;
using CoverShooter;
using Features.Actor.Rules;
using FSM;
using FSM.Data;
using FSM.States;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

namespace Features.Actor.Services
{
    public class PlayerStateService : IInitializable, IDisposable
    {
        private readonly ActorRule _actorRule;
        private readonly IStateMachine _stateMachine;
        
        private CharacterMotor _characterMotor;

        public PlayerStateService(ActorRule actorRule,
            IStateMachine stateMachine)
        {
            _actorRule = actorRule;
            _stateMachine = stateMachine;
            _characterMotor = null;

            Initialize();
        }
        public void Initialize()
        {
            if (_actorRule.GetActorView() != null)
            {
                _characterMotor = _actorRule.GetActorView().GetComponent<CharacterMotor>();
            }

            _actorRule.ActorCreated += (_, view) =>
            {
                _characterMotor = view.GetComponent<CharacterMotor>();

                _characterMotor.Died += () => _stateMachine.GoGameOver(CurtainType.NoFadeOut);
            };
        }

        public bool IsCrouching()
            => _characterMotor != null && _characterMotor.IsCrouching && _characterMotor.IsGrounded;

        public bool IsJumping()
            => _characterMotor != null && _characterMotor.IsJumping;

        public bool IsWalking()
            => _characterMotor != null && !_characterMotor.IsWalkingOrStanding;

        public bool IsSprinting()
            => _characterMotor != null && _characterMotor.IsMoving && Input.GetButton("Run");

        public bool IsInCover()
            => _characterMotor != null && _characterMotor.IsInCover;

        public bool IsVaulting()
            => _characterMotor != null && _characterMotor.IsVaulting;

        public void Dispose()
        {
            _characterMotor = null;
        }
    }
}