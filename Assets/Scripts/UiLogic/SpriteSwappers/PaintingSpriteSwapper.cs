using GalleryLogic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ScriptableObjects;

namespace UiLogic.SpriteSwappers
{
    /// <summary>
    /// Змінює спрайт картини.
    /// Навішений на <see cref="GameObject"/> ColorButton на кожній картині в <see cref="Gallery"/>.
    /// </summary>
    public class PaintingSpriteSwapper : SpriteSwapper
    {
        /// <summary>
        /// Текст кнопки під картиною
        /// </summary>
        [Header("Current painting")]
        [SerializeField] protected Text buttonText;
        
        /// <summary>
        /// Текст для кольорування
        /// </summary>
        private const string COLORIZED = "Colorize";
        /// <summary>
        /// Текст для чорно-білого
        /// </summary>
        private const string BLACK_AND_WHITE = "B&W";

        /// <summary>
        /// Змінює <see cref="Sprite"/> <see cref="currentImage"/> на <see cref="blackAndWhitePainting"/> або <see cref="colorizedPainting"/>.
        ///Додається на OnClick на компоненті <see cref="Button"/>.
         /// </summary>
        public override void Swap()
        {
            if (currentImage.sprite == spritesToSwapObject.DefaultSprite)
            {
                currentImage.sprite = spritesToSwapObject.FirstSpriteToSwap;
                buttonText.text = BLACK_AND_WHITE;
            }
            else if (currentImage.sprite == spritesToSwapObject.FirstSpriteToSwap)
            {
                currentImage.sprite = spritesToSwapObject.DefaultSprite;
                buttonText.text = COLORIZED;
            }
        }
    }
}