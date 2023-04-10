using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Registries
{
    public interface IRegistry
    {
    }

    public interface IRegistryList : IEnumerable, IRegistry
    {
    }

    public interface IRegistryData
    {
        string Id { get; }
    }

    public abstract class RegistryListBase<TData> : ScriptableObject, IRegistryList where TData : class, IRegistryData
    {
        [SerializeField] protected TData[] RegistryItems;

        public int Length => RegistryItems.Length;

        public IEnumerator GetEnumerator()
        {
            return RegistryItems.GetEnumerator();
        }

        public TData[] GetItems()
        {
            return RegistryItems;
        }

        public Dictionary<string, TData> ToDictionary()
        {
            return RegistryItems.ToDictionary(key => key.Id, value => value);
        }

        public TData GetById(string id)
        {
            return RegistryItems.FirstOrDefault(item => string.CompareOrdinal(item.Id, id) == 0);
        }
    }
}