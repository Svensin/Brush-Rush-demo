using UnityEngine;
using UnityEngine.UI;

namespace GalleryLogic
{
    [DisallowMultipleComponent]
    public class GalleryUi : BaseUi
    {
        /// <summary>
        /// Panel with all paintigs.
        /// </summary>
        public Canvas GalleryPanelCanvas => panelCanvas;
        
        public Button CloseGalleryButton => closePanelButton;
        
        /// <summary>
        /// Closes <see cref="Gallery"/> menu.
        /// </summary>
        public override void Close()
        { 
            GalleryPanelCanvas.enabled = false;
            ScriptReferences.Instance.menuCarrousel.enabled = true;
        }
         
        
        /// <summary>
        /// Opens <see cref="Gallery"/> menu.
        /// </summary>
        public override void Open()
        {
            GalleryPanelCanvas.enabled = true;
            ScriptReferences.Instance.menuCarrousel.enabled = false;
        }

        private void Start()
        {
            CloseGalleryButton.onClick.AddListener(Close);
        }
    }
}
