using System.Collections.Generic;

namespace Milhouzer.Core.AI
{
    /// <summary>
    /// Blackboard class : stores data of any type at runtime.
    /// </summary>
    public class Blackboard
    {
        /// <summary>
        /// Dictionary used to store the data
        /// </summary>
        /// <typeparam name="string">names of the data</typeparam>
        /// <typeparam name="object">value</typeparam>
        /// <returns></returns>
        private readonly Dictionary<string, object> data = new Dictionary<string, object>();

        /// <summary>
        /// Set the value of a data in the dictionary. Automatically creates the key if the data does not exist.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public void SetValue<T>(string key, T value)
        {
            if(!data.ContainsKey(key))
            {
                data.Add(key, value);
                return;
            }

            data[key] = value;
        }

        /// <summary>
        /// Get the value of a data stored in the dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            return data.ContainsKey(key) ? (T)data[key] : default;
        }

        /// <summary>
        /// Delete a value using its name.
        /// </summary>
        /// <param name="key"></param>
        public void DeleteValue(string key)
        {
            if(data.ContainsKey(key))
                data.Remove(key);
        }
    }
}
