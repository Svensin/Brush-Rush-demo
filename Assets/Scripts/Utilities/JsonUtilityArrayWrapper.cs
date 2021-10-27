using System;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Wrapper of <see cref="JsonUtility"/> for work with arrays 
    /// </summary>
    public static class JsonUtilityArrayWrapper
    {
        /// <summary>
        /// Serialization in JSON
        /// </summary>
        /// <param name="array">array of objects</param>
        /// <param name="prettyPrint">or structured JSON</param>
        /// <typeparam name="T">type of objects</typeparam>
        /// <returns>JSON-line</returns>
        public static string ToJson<T>(T[] array, bool prettyPrint = false)
        {
            var wrapper = new Wrapper<T> {Items = array};
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        /// <summary>
        /// Десеріалізація з JSON
        /// </summary>
        /// <param name="json">JSON-рядок</param>
        /// <typeparam name="T">тип у який десеріалізовувати</typeparam>
        /// <returns>масив <see cref="T"/></returns>
        public static T[] FromJson<T>(string json)
        {
            var wrapper = new Wrapper<T>();
            return JsonUtility.FromJson<Wrapper<T>>(json).Items;
        }

        /// <summary>
        /// Серіалізована обгортка для роботи з <see cref="JsonUtility"/>
        /// </summary>
        /// <typeparam name="T">тип об'єкта</typeparam>
        [Serializable]
        public class Wrapper<T>
        {
            /// <summary>
            /// Масив об'єктів
            /// </summary>
            public T[] Items;
        }
    }
}