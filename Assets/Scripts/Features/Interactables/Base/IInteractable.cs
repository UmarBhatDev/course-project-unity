using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Features.Interactables.Base
{
    public interface IInteractable
    {
        bool CanInteract(Vector3 interactPosition, out float interactDistance);
        UniTask Interact(CancellationToken cancellationToken);
        void StopInteract();
    }
}