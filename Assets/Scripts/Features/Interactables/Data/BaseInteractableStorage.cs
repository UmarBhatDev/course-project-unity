using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace Features.Interactables.Data
{
    public class BaseInteractableStorage<T> : IEnumerable<T>
    {
        public int Count => Items.Count; 
        protected readonly List<T> Items = new();
        protected readonly Subject<T> ItemAddedSubject = new();
        protected readonly Subject<T> ItemRemovedSubject = new();

        public IObservable<T> OnItemAdded => ItemAddedSubject;
        public IObservable<T> OnItemRemoved => ItemRemovedSubject;
        
        public virtual void AddItem(T item)
        {
            if (Items.Contains(item))
                return;
            
            Items.Add(item);
            ItemAddedSubject.OnNext(item);
        }
        
        public virtual void RemoveItem(T item)
        {
            var succeed = Items.Remove(item);
            if (succeed)
            {
                ItemRemovedSubject.OnNext(item);
            }
        }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}