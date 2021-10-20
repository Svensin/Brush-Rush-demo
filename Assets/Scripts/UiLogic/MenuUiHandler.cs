using System;
using UnityEngine;
using UnityEngine.UI;
using OptionsLogic;
using GalleryLogic;

namespace UiLogic
{
    /// <summary>
    /// Handler for main menu
    /// </summary>
    [DisallowMultipleComponent]
    public class MenuUiHandler : MonoBehaviour
    {
        private Gallery _gallery;
        private Options _options;
        /// <summary>
        /// Button which opens <see cref="Gallery"/>.
        /// </summary>
        [SerializeField] private Button openGalleryButton;
        /// <summary>
        /// Button which opens <see cref="Options"/>.
        /// </summary>
        [SerializeField] private Button[] optionsButtons;

        [SerializeField] private Canvas menuPanel;

        public Canvas MenuPanel => menuPanel;

        private void Awake()
        {
            _gallery = Gallery.Instance;
            _options = Options.Instance;
        }
 
        private void Start()
        {
            _options.OptionsUi.Subscribe();
            openGalleryButton.onClick.AddListener(_gallery.GalleryUi.Open);
            foreach (var btn in optionsButtons)
            {
                btn.onClick.AddListener(_options.OptionsUi.Open);
            }
        }

        private void OnDestroy()
        {
            _options.OptionsUi.Unsubscribe();
        }
    }
}