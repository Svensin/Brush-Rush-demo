using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpritesToSwapObject", order = 0)]
    public class SpritesToSwapObject : ScriptableObject
    {
                /// <summary>
                /// Grayscale painting
                /// </summary>
                [Header("Sprites to swap")][Space]
                [SerializeField] protected Sprite defaultSprite;

                public Sprite DefaultSprite => defaultSprite;
                /// <summary>
                /// Colored painting
                /// </summary>
                [SerializeField] private Sprite firstSpriteToSwap;

                public Sprite FirstSpriteToSwap => firstSpriteToSwap;
    }
}