﻿using System;
using Features.Actor.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Actor.Views
{
    public class ActorView : MonoBehaviour, IDisposable
    {
        [Inject]
        private ActorModel _actorModel;
        private CompositeDisposable _compositeDisposable;

        [SerializeField]
        private GameObject _actor;
        
        private Animator _animator;
        private Rigidbody _rigidbody;

        private Vector3 _newPosition;
        private Quaternion _newRotation;
        
        private readonly int _movementStateAnimatorHash
            = Animator.StringToHash("MovementState");
        
        private void Start()
        {
            _animator = _actor.GetComponent<Animator>();
            _rigidbody = _actor.GetComponent<Rigidbody>();
            
            _compositeDisposable = new CompositeDisposable();
            
            _rigidbody.MovePosition(_actorModel.GetPosition());
            _actor.transform.rotation = _actorModel.GetRotation();
            _animator.SetInteger(_movementStateAnimatorHash, (int) MovementState.Idle);
            
            _actorModel
                .GetPositionAsObservable()
                .Subscribe(position => _rigidbody.MovePosition(position))
                .AddTo(_compositeDisposable);

            _actorModel
                .GetRotationAsObservable()
                .Subscribe(rotation => _actor.transform.rotation = rotation)
                .AddTo(_compositeDisposable);

            _actorModel
                .GetMovementStateAsObservable()
                .Subscribe(movementState => _animator.SetInteger(_movementStateAnimatorHash, (int)movementState))
                .AddTo(_compositeDisposable);
        }
        
        public void Dispose() 
            => _compositeDisposable.Dispose();
    }
}