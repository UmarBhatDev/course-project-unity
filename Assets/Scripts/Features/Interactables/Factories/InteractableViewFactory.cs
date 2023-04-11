using Features.Interactables.Base;
using Features.Interactables.Data;
using UnityEngine;
using Zenject;

namespace Features.Interactables.Factories
{
    public class InteractableViewFactory : IFactory<GameObject, Transform, IInteractable>
    {
        private readonly DiContainer _diContainer;
        private readonly InteractableStorage _interactableStorage;

        public InteractableViewFactory(DiContainer diContainer, InteractableStorage interactableStorage)
        {
            _diContainer = diContainer;
            _interactableStorage = interactableStorage;
        }

        public virtual IInteractable Create(GameObject prefab, Transform transform)
        {
            var gameObject = _diContainer.InstantiatePrefab(prefab);
            
            gameObject.transform.position = transform.position;
            gameObject.transform.rotation = transform.rotation;

            var interactable = prefab.GetComponent<IInteractable>();

            _interactableStorage.AddItem(interactable);

            return interactable;
        }
    }
}