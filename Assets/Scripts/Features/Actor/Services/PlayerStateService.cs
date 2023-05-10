using System;
using CoverShooter;
using Features.Actor.Rules;
using Unity.VisualScripting;
using UnityEngine;

namespace Features.Actor.Services
{
    public class PlayerStateService : IInitializable
    {
        private readonly ActorRule _actorRule;

        private CharacterMotor _characterMotor;

        public PlayerStateService(ActorRule actorRule)
        {
            _actorRule = actorRule;
            _characterMotor = null;
            Initialize();
        }

        public void Initialize()
        {
            if (_actorRule.GetActorView() != null)
            {
                _characterMotor = _actorRule.GetActorView().GetComponent<CharacterMotor>();
            }

            _actorRule.ActorCreated += (_, view) => { _characterMotor = view.GetComponent<CharacterMotor>(); };
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