using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Features.Interactables.Base
{
    public interface IInteractable : IDisposable
    {
        public bool CanInteract(Vector3 interactPosition, out float interactDistance);
        public UniTask Interact(CancellationToken externalToken);
        public void StopInteraction();
    }
}