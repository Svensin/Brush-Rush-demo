using System;
using UnityEngine;

namespace Utilities
{
    /// <summary>
    /// Обгортка над <see cref="JsonUtility"/> для роботи з масивами 
    /// </summary>
    public static class JsonUtilityArrayWrapper
    {
        /// <summary>
        /// Серіалізація в JSON
        /// </summary>
        /// <param name="array">масив об'єктів</param>
        /// <param name="prettyPrint">чи структорований JSON</param>
        /// <typeparam name="T">тип об'єктів</typeparam>
        /// <returns>JSON-рядок</returns>
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