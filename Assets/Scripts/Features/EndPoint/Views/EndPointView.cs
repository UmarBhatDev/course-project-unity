using System;
using Features.Lootboxes.Views;
using UniRx;
using UnityEngine;

namespace Features.EndPoint.Views
{
    public class EndPointView : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject _bonfire;
        
        public readonly ReactiveCommand EndPointReached = new();

        private BaseLootboxView _lootbox;
        private CompositeDisposable _compositeDisposable = new();

        private void Awake()
        {
            _bonfire.SetActive(false);
        }

        private void Start()
        {
            _lootbox = GetComponent<BaseLootboxView>();
            
            _lootbox
                .Interacted
                .Subscribe(_ =>
                {
                    _bonfire.SetActive(true);
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