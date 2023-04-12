using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Interactables.Base;
using Features.Interactables.Data;
using UnityEngine;
using Zenject;

namespace Features.Lootboxes.Views
{
    public class TestLootboxView : MonoBehaviour, IInteractable
    {
        [Inject]
        private InteractableStorage _interactableStorage;
        private CancellationTokenSource _cancellationTokenSource;
        private MeshRenderer _meshRenderer;
        private bool _canInteract = true;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material.color = Color.red;
            
            _interactableStorage.AddItem(this);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public bool CanInteract(Vector3 interactPosition, out float interactDistance)
        {
            interactDistance = Vector3.Distance(interactPosition, transform.position);
            return _canInteract;
        }

        public async UniTask Interact(CancellationToken externalToken)
        {
            _canInteract = false;
            
            var linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(externalToken, _cancellationTokenSource.Token);
            
            var tween = _meshRenderer.material.DOColor(Color.green, 0.1f);
            await tween.ToUniTask(TweenCancelBehaviour.Kill, linkedCts.Token);

            await UniTask.WaitUntilCanceled(linkedCts.Token);
            
            _canInteract = true;
        }

        public void StopInteraction()
        {
            _canInteract = true;
            _meshRenderer.material.DOColor(Color.red, 0.1f);
            
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _interactableStorage.RemoveItem(this);
        }
    }
}