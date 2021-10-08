using UnityEngine;
using UnityEngine.UI;
using Utilities.ScriptableObjects;

namespace UiLogic.SpriteSwappers
{
    /// <summary>
    /// Базовий клас для зміни спрайтів.
    /// </summary>
    public abstract class SpriteSwapper : MonoBehaviour
    {
        /// <summary>
        /// <see cref="ScriptableObject"/> який містить у собі спрайти для зміни.
        /// </summary>
        [SerializeField] protected SpritesToSwapObject spritesToSwapObject;
        
        /// <summary>
        /// Поточний <see cref="Image"/>.
        /// </summary>
        [SerializeField] protected Image currentImage;

        /// <summary>
        /// <summary>
        /// Змінює <see cref="Sprite"/> <see cref="currentImage"/> на відповідно зазначений.
        ///Додається на OnClick на компоненті <see cref="Button"/>.
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