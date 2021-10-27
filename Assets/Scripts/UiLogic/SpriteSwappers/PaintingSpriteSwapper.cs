using GalleryLogic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.ScriptableObjects;

namespace UiLogic.SpriteSwappers
{
    /// <summary>
    /// Changes painting sprite.
    /// Is assingned <see cref="GameObject"/> ColorButton on every painting in <see cref="Gallery"/>.
    /// </summary>
    public class PaintingSpriteSwapper : SpriteSwapper
    {
        /// <summary>
        /// Button text under the painting
        /// </summary>
        [Header("Current painting")]
        [SerializeField] protected Text buttonText;
        
        /// <summary>
        /// Text for colorizing
        /// </summary>
        private const string COLORIZED = "Colorize";
        /// <summary>
        /// Text for black and white
        /// </summary>
        private const string BLACK_AND_WHITE = "B&W";

        /// <summary>
        /// Swaps <see cref="Sprite"/> <see cref="currentImage"/> for <see cref="blackAndWhitePainting"/> or <see cref="colorizedPainting"/>.
        ///Is added on OnClick component of <see cref="Button"/>.
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