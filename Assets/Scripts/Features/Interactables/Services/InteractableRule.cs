using System;
using System.Collections.Generic;
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

namespace Features.Interactables.Services
{
    public class InteractableRule : IInitializable, IDisposable, IGameRule
    {
        private ActorModel _actorModel;

        private readonly ActorRule _actorRule;
        private readonly InteractableStorage _interactableStorage;

        private CompositeDisposable _compositeDisposable;
        private CancellationTokenSource _cancellationTokenSource;

        public InteractableRule(InteractableStorage interactableStorage, ActorRule actorRule)
        {
            _actorRule = actorRule;
            _interactableStorage = interactableStorage;
            _compositeDisposable = new CompositeDisposable();
            _cancellationTokenSource = new CancellationTokenSource();
        }
        
        public void Initialize()
        {
            _actorRule.ActorCreated += model =>
            {
                _actorModel = model;
                
                _compositeDisposable?.Dispose();
                _cancellationTokenSource?.Cancel();

                _compositeDisposable = new CompositeDisposable();
                _cancellationTokenSource = new CancellationTokenSource();
                
                _actorModel
                    .GetPositionAsObservable()
                    .Subscribe(playerPosition => HandleInteractions(playerPosition).Forget())
                    .AddTo(_compositeDisposable);
            };
        }

        private async UniTask HandleInteractions(Vector3 playerPosition)
        {
            var interactablesInRange = new Dictionary<IInteractable, float>();

            foreach (var interactable in _interactableStorage)
            {
                var canInteract = interactable.CanInteract(playerPosition, out var interactDistance);

                if (canInteract)
                    interactablesInRange.Add(interactable, interactDistance);
            }

            var closestInteractable = interactablesInRange
                .OrderBy(x => x.Value)
                .First()
                .Key;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            await closestInteractable.Interact(_cancellationTokenSource.Token);
            closestInteractable.StopInteract();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
            _cancellationTokenSource?.Dispose();

            _compositeDisposable = new CompositeDisposable();
            _cancellationTokenSource = new CancellationTokenSource();
        }
    }
}