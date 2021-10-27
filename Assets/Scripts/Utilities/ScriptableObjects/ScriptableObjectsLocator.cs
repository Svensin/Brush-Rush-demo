using System.Collections.Generic;
using UnityEngine;

namespace Utilities.ScriptableObjects
{
    /// <summary>
    /// <see cref="ScriptableObject"/> which contains all<see cref="ScriptableObject"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "Scriptable Object Locator", order = 0)]
    public class ScriptableObjectsLocator : ScriptableObject
    {
        /// <summary>
        /// All <see cref="ScriptableObject"/> for paintings
        /// </summary>
        [SerializeField] private List<SpritesToSwapObject> paintingSpritesList;

        /// <summary>
        /// All <see cref="ScriptableObject"/> for paintings
        /// </summary>
        public List<SpritesToSwapObject> PaintingSpritesList => paintingSpritesList;
    }
}