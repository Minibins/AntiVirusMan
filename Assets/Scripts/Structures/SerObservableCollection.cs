using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityEngine;
namespace DustyStudios.SerCollections
{
    [Serializable]
    public class SerObservableCollection<T> : ObservableCollection<T>, ISerializationCallbackReceiver, INotifyCollectionChangedEntry
    {
        [SerializeField]
        public T[] itemsList;
        public SerObservableCollection() : base() { }
        public SerObservableCollection(List<T> list)
            : this((IEnumerable<T>)list) { }
        public SerObservableCollection(IEnumerable<T> collection) : base(collection)
        {
            CopyFrom(collection);
        }
        private void CopyFrom(IEnumerable<T> collection)
        {
            List<T> Collection = new();
            if(collection == null || itemsList == null)
                return;
            foreach(T item in collection)
                Collection.Add(item);
            itemsList = Collection.ToArray();
        }
        public void OnBeforeSerialize()
        {
            itemsList = new T[this.Count];
            CopyTo(itemsList,0);
        }
        public void OnAfterDeserialize()
        {
            Items.Clear();
            if(itemsList == null) throw new NullReferenceException();
            foreach(var item in itemsList)
                Items.Add(item);
        }

        public void OnCollectionChanged() => OnCollectionChanged();
    }
    public interface INotifyCollectionChangedEntry
    {
        public void OnCollectionChanged();
    }
}