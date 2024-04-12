using System;
using UnityEngine;

namespace Milhouzer.Common.Utility
{
    public class Database<T> : ScriptableObject
    {
        [SerializeField]
        protected T[] Entries;

        public T FindEntry(Predicate<T> predicate)
        {
            return Array.Find(Entries, predicate);
        }
    }
}
