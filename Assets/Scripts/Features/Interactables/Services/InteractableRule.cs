using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Actor.Models;
using Features.Actor.Rules;
using Features.Interactables.Base;
using Features.Interactables.Data;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unit = UniRx.Unit;

namespace Features.Interactables.Services
{
    public class InteractableRule : IInitializable, IDisposable, IGameRule
    {
        private ActorModel _actorModel;

        private readonly ActorRule _actorRule;
        private readonly InteractableConfig _interactableConfig;
        private readonly InteractableStorage _interactableStorage;

        private ReactiveCommand _interacted;
        private IInteractable _currentInteractable;
        private IDisposable _statusCheckDisposable;
        private CompositeDisposable _compositeDisposable;

        public InteractableRule(InteractableStorage interactableStorage, InteractableConfig interactableConfig, ActorRule actorRule)
        {
            _actorRule = actorRule;
            _interactableConfig = interactableConfig;
            _interactableStorage = interactableStorage;
            
            _interacted = new ReactiveCommand();
            _compositeDisposable = new CompositeDisposable();
            
            Initialize();
        }
        
        public void Initialize()
        {
            _actorRule.ActorCreated += (model, _) =>
            {
                _actorModel = model;
                
                _compositeDisposable?.Dispose();
                _compositeDisposable = new CompositeDisposable();
                
                _actorModel
                    .GetPositionAsObservable()
                    .Subscribe(playerPosition => HandleInteractions(playerPosition).Forget())
                    .AddTo(_compositeDisposable);
            };
        }

        public IObservable<Unit> GetInteractedAsObservable()
            => _interacted.AsUnitObservable();

        private async UniTask HandleInteractions(Vector3 playerPosition)
        {
            var interactableTuples = _interactableStorage
                .Select(interactable =>
                {
                    var canInteract = interactable.CanInteract(playerPosition, out var distance);
                    return (interactable, canInteract, distance);
                });
                
            var interactableTuple = interactableTuples
                .Where(interactable => interactable.distance < _interactableConfig.InteractDistance)
                .OrderBy(interactable => interactable.distance)
                .FirstOrDefault();

            if (interactableTuple == default) return;
            if (!interactableTuple.canInteract) return;

            await StopCurrentInteractable();
            
            _currentInteractable = interactableTuple.interactable;
            _statusCheckDisposable = Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    _currentInteractable.CanInteract(_actorModel.GetPosition(), out var interactDistance);
                    var isActorReady = interactDistance <= _interactableConfig.InteractDistance;

                    if (isActorReady) return;

                    StopCurrentInteractable().Forget();
                });

            await _currentInteractable.Interact(CancellationToken.None);

            _interacted?.Execute();
        }

        private async UniTask StopCurrentInteractable()
        {
            try
            {
                if (_currentInteractable != null) 
                    await _currentInteractable.StopInteraction();
            }
            catch (Exception)
            {
                // ignored
                //because MonoBehaviour never gets null
            }

            _statusCheckDisposable?.Dispose();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _compositeDisposable = new CompositeDisposable();
        }
    }
}