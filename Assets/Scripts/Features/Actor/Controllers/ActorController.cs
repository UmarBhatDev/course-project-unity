using System;
using Features.Actor.Models;
using Features.Actor.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Actor.Controllers
{
    public class ActorController : IInitializable, IDisposable
    {
        private readonly ActorModel _actorModel;
        private readonly CompositeDisposable _compositeDisposable;

        private const float RotationSpeed = 15f;
        private const float RunSpeed = 5;
        private const float WalkSpeed = 2;
        
        private Vector3 _direction;
        private float _localSpeed;

        private readonly ActorView _actorView;

        public ActorController(ActorModel actorModel, ActorView actorView)
        {
            _actorView = actorView;
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
            Observable
                .EveryFixedUpdate()
                .Subscribe(_ => LocalFixedUpdate())
                .AddTo(_compositeDisposable);
        }

        private void LocalFixedUpdate()
        {
            if(_direction == Vector3.zero) return;
            
            var rigidBody = _actorModel.GetRigidbody();
            var transform = rigidBody.transform;

            var moveDirection = _direction.normalized.x * _actorView.Orientation.forward + _direction.normalized.z * _actorView.Orientation.right;
            
            _actorModel.GetRigidbody().MovePosition(transform.position + moveDirection * _localSpeed * Time.deltaTime);

            var newPosition = transform.position;
            
            var newRotation = Quaternion.LookRotation(moveDirection);

            _actorModel.SetRotation(newRotation);
            _actorModel.SetPosition(newPosition);
        }

        private void LocalUpdate()
        {
            _direction = new Vector3
            {
                z = Input.GetAxisRaw("Horizontal"),
                x = Input.GetAxisRaw("Vertical")
            };

            if (_direction == Vector3.zero)
            {
                _actorModel.SetMovementState(MovementState.Idle);
                return;
            }

            var isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            _localSpeed = isRunning ? RunSpeed : WalkSpeed;

            _actorModel.SetMovementState(isRunning ? MovementState.Run : MovementState.Walk);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}