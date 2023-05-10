using System;
using Features.Lootboxes.Views;
using UniRx;
using UnityEngine;

namespace Features.EndPoint.Views
{
    public class EndPointView : MonoBehaviour, IDisposable
    {
        public readonly ReactiveCommand EndPointReached = new();

        private BaseLootboxView _lootbox;
        private CompositeDisposable _compositeDisposable = new();

        private void Start()
        {
            _lootbox = GetComponent<BaseLootboxView>();
            
            _lootbox
                .Interacted
                .Subscribe(_ =>
                {
                    EndPointReached.Execute();
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            EndPointReached?.Dispose();
            _compositeDisposable.Dispose();
            Destroy(gameObject);
        }
    }
}