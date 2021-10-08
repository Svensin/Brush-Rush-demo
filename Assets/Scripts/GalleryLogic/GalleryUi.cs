using UnityEngine;
using UnityEngine.UI;

namespace GalleryLogic
{
    [DisallowMultipleComponent]
    public class GalleryUi : BaseUi
    {
        /// <summary>
        /// Панель із картинами.
        /// </summary>
        public Canvas GalleryPanelCanvas => panelCanvas;
        
        public Button CloseGalleryButton => closePanelButton;
        
        /// <summary>
        /// Закриває <see cref="Gallery"/>.
        /// </summary>
        public override void Close()
        { 
            GalleryPanelCanvas.enabled = false;
            ScriptReferences.Instance.menuCarrousel.enabled = true;
        }
         
        
        /// <summary>
        /// Відкриває <see cref="Gallery"/>.
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
