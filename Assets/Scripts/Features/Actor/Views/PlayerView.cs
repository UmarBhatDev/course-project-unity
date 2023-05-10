using System;
using Features.Actor.Models;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Actor.Views
{
    public class PlayerView : MonoBehaviour, IDisposable
    {
        [Inject]
        private ActorModel _actorModel;
        private CompositeDisposable _compositeDisposable;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();
            
            _actorModel
                .GetIsDisposedAsObservable()
                .Subscribe(_ => Dispose())
                .AddTo(_compositeDisposable);
            
            _rigidbody = GetComponent<Rigidbody>();
            
            _rigidbody.MovePosition(_actorModel.GetPosition());
            transform.rotation = _actorModel.GetRotation();
        }

        private void Update()
        {
            _actorModel.SetPosition(transform.position);
            _actorModel.SetRotation(transform.rotation);
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
            Destroy(gameObject);
        }
    }
}