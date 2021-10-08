using System;
using UnityEngine;

namespace Utilities.SaveLoadData
{
    /// <summary>
    /// Серіалізована модель фільтра картини для збереження/завантаження у <see cref="SaversLoaders.GallerySaverLoader"/>.
    /// </summary>
    [Serializable]
    public class Filter
    {
        /// <summary>
        /// Чи увімкнений фільтр картини у галереї
        /// </summary>
        public bool isEnabled;

        /// <summary>
        /// Оновлює ігровий об'єкт <see cref="GameObject"/> із моделі <see cref="Filter"/>.
        /// </summary>
        /// <param name="filterData">модель-фільтр</param>
        /// <param name="gameObject">ігровий об'єкт фільтра</param>
        /// <returns>змінений ігровий об'єкт фільтра(optional)</returns>
        public static GameObject UpdateFilter(Filter filterData, GameObject gameObject)
        {
            gameObject.SetActive(filterData.isEnabled);

            return gameObject;
        }
    }
}