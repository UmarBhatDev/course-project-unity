using System;
using Features.Actor.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Actor.Controllers
{
    public class ActorController : IInitializable, IDisposable
    {
        private readonly ActorModel _actorModel;
        private readonly CompositeDisposable _compositeDisposable;
        
        private const float RotationSpeed = 5f;
        private const float RunSpeed = 5;
        private const float WalkSpeed = 2;
        private Vector3 _currentDirection;

        public ActorController(ActorModel actorModel)
        {
            _actorModel = actorModel;
            _compositeDisposable = new CompositeDisposable();

            Initialize();
        }

        public void Initialize()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => LocalUpdate())
                .AddTo(_compositeDisposable);
        }

        private void LocalUpdate()
        {
            var newDirection = new Vector3
            {
                z = Input.GetAxisRaw("Horizontal"),
                x = -Input.GetAxisRaw("Vertical")
            };

            if (newDirection == _currentDirection)
            {
                _actorModel.SetMovementState(MovementState.Idle);
                return;
            }
                    
            var isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            _actorModel.SetMovementState(isRunning ? MovementState.Run : MovementState.Walk);

            UpdatePlayerPosition(newDirection);
            _currentDirection = newDirection;
        }

        private void UpdatePlayerPosition(Vector3 direction)
        {
            var isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            
            var speed = isRunning ? RunSpeed : WalkSpeed;
            var moveDirection = direction.normalized;
            
            var newPosition = _actorModel.GetPosition() + moveDirection * speed * Time.deltaTime;
            
            var lookRotation = Quaternion.LookRotation(moveDirection);
            var newRotation = Quaternion.Slerp(_actorModel.GetRotation(), lookRotation, Time.deltaTime * RotationSpeed);

            _actorModel.SetRotation(newRotation);
            _actorModel.SetPosition(newPosition);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}