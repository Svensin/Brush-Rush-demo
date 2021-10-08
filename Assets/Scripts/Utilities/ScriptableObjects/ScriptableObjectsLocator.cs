using System.Collections.Generic;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    /// <summary>
    /// <see cref="ScriptableObject"/> що містить у собі всі <see cref="ScriptableObject"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "Scriptable Object Locator", order = 0)]
    public class ScriptableObjectsLocator : ScriptableObject
    {
        /// <summary>
        /// Усі <see cref="ScriptableObject"/> для картин
        /// </summary>
        [SerializeField] private List<SpritesToSwapObject> paintingSpritesList;

        /// <summary>
        /// Усі <see cref="ScriptableObject"/> для картин
        /// </summary>
        public List<SpritesToSwapObject> PaintingSpritesList => paintingSpritesList;
    }
}