using System;
using UniRx;
using UnityEngine;

namespace Features.Actor.Models
{
    public class ActorModel : IDisposable
    {
        private readonly ReactiveProperty<Vector3> _position;
        private readonly ReactiveProperty<Quaternion> _rotation;
        private readonly ReactiveProperty<MovementState> _movementState;


        public ActorModel(Vector3 position, Quaternion rotation)
        {
            _position = new ReactiveProperty<Vector3>(position);
            _rotation = new ReactiveProperty<Quaternion>(rotation);
            _movementState = new ReactiveProperty<MovementState>(MovementState.Idle);
        }

        public void SetPosition(Vector3 position)
            => _position.Value = position;

        public void SetRotation(Quaternion rotation)
            => _rotation.Value = rotation;
        
        public void SetMovementState(MovementState movementState)
            => _movementState.Value = movementState;

        public Vector3 GetPosition()
            => _position.Value;

        public Quaternion GetRotation()
            => _rotation.Value;

        public MovementState GetMovementState()
            => _movementState.Value;
        
        public IObservable<Vector3> GetPositionAsObservable()
            => _position.AsObservable();
        
        public IObservable<Quaternion> GetRotationAsObservable()
            => _rotation.AsObservable();
        
        public IObservable<MovementState> GetMovementStateAsObservable()
            => _movementState.AsObservable();

        public void Dispose()
        {
            _position?.Dispose();
            _rotation?.Dispose();
            _movementState?.Dispose();
        }
    }
    
    public enum MovementState
    {
        Idle = 0,
        Walk = 1,
        Run = 2,
    }
}