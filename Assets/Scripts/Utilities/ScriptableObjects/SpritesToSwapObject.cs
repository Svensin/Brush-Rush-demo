using UnityEngine;

namespace Utilities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpritesToSwapObject", order = 0)]
    public class SpritesToSwapObject : ScriptableObject
    {
                /// <summary>
                /// Чорнобіла картина
                /// </summary>
                [Header("Sprites to swap")][Space]
                [SerializeField] protected Sprite defaultSprite;

                public Sprite DefaultSprite => defaultSprite;
                /// <summary>
                /// Кольорова картина
                /// </summary>
                [SerializeField] private Sprite firstSpriteToSwap;

                public Sprite FirstSpriteToSwap => firstSpriteToSwap;
    }
}