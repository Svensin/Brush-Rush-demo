using System;
using UnityEngine;

namespace Utilities.SaveLoadData
{
    /// <summary>
    /// serealized model of painting filter for saving/loading in/from <see cref="SaversLoaders.GallerySaverLoader"/>.
    /// </summary>
    [Serializable]
    public class Filter
    {
        /// <summary>
        /// is filter enabled on painting
        /// </summary>
        public bool isEnabled;

        /// <summary>
        /// Update game object <see cref="GameObject"/> from model of <see cref="Filter"/>.
        /// </summary>
        /// <param name="filterData">model-filter</param>
        /// <param name="gameObject">game object of the filter</param>
        /// <returns>changed game object of the filter(optional)</returns>
        public static GameObject UpdateFilter(Filter filterData, GameObject gameObject)
        {
            gameObject.SetActive(filterData.isEnabled);

            return gameObject;
        }
    }
}