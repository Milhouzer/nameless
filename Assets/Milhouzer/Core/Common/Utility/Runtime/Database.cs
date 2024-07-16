using System;
using UnityEngine;

namespace Milhouzer.Common.Utility
{
    [Serializable]
    public class Database<T> : ScriptableObject
    {
        [SerializeField]
        protected T[] Entries;

        public T[] GetEntries => Entries;

        public T FindEntry(Predicate<T> predicate)
        {
            return Array.Find(Entries, predicate);
        }
    }
}
