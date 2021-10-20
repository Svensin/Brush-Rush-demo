using UnityEngine;
using UnityEngine.UI;
using Utilities.ScriptableObjects;

namespace UiLogic.SpriteSwappers
{
    /// <summary>
    /// Base class for sprite swap.
    /// </summary>
    public abstract class SpriteSwapper : MonoBehaviour
    {
        /// <summary>
        /// <see cref="ScriptableObject"/> which contains all sprites for swapping.
        /// </summary>
        [SerializeField] protected SpritesToSwapObject spritesToSwapObject;
        
        /// <summary>
        /// Current <see cref="Image"/>.
        /// </summary>
        [SerializeField] protected Image currentImage;

        /// <summary>
        /// <summary>
        /// Swaps <see cref="Sprite"/> <see cref="currentImage"/> for wanted one.
        ///Is added on OnClick component <see cref="Button"/>.
        /// </summary>
        /// </summary>
        public virtual void Swap()
        {
            if (currentImage.sprite == spritesToSwapObject.DefaultSprite)
            {
                currentImage.sprite = spritesToSwapObject.FirstSpriteToSwap;
            }
            else if (currentImage.sprite == spritesToSwapObject.FirstSpriteToSwap)
            {
                currentImage.sprite = spritesToSwapObject.DefaultSprite;
            }
        }
    }
}