namespace UiLogic.SpriteSwappers
{
    public class ButtonSpriteSwapper : SpriteSwapper
    {
        public bool IconActive => currentImage.sprite == spritesToSwapObject.DefaultSprite;
        public void Swap(bool iconActive)
        {
            currentImage.sprite = iconActive ? spritesToSwapObject.DefaultSprite : spritesToSwapObject.FirstSpriteToSwap;
        }
    }
}