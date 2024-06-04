using System;
using Features.Lootboxes.Views;
using Features.PaperHint.Factories;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.PaperHint.Views
{
    public class PaperHintInteractable : MonoBehaviour, IDisposable
    {
        private BaseLootboxView _lootbox;
        private readonly CompositeDisposable _compositeDisposable = new();

        [Inject] private PaperHintControllerFactory _paperHintControllerFactory;

        private void Start()
        {
            _lootbox = GetComponent<BaseLootboxView>();
            
            _lootbox
                .Interacted
                .Subscribe(_ =>
                {
                    var paperHintController = _paperHintControllerFactory.Create();
                    paperHintController.StartFlow();
                    
                    _compositeDisposable?.Dispose();
                })
                .AddTo(_compositeDisposable);
        }
        
        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            Destroy(gameObject);
        }
    }
}