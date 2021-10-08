using System;
using UiLogic.SpriteSwappers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace OptionsLogic
{
    [DisallowMultipleComponent]
    public class OptionsUi : BaseUi, ISubscribable
    {
        private Canvas menuCanvas;
        private GameObject levelUi;
        [SerializeField]private ButtonSpriteSwapper soundsIconSwapper;
        [SerializeField]private ButtonSpriteSwapper vibrationsIconSwapper;
        [SerializeField]private ButtonSpriteSwapper musicIconSwapper;

        public ButtonSpriteSwapper SoundsIconSwapper => soundsIconSwapper;
        public ButtonSpriteSwapper VibrationsIconSwapper => vibrationsIconSwapper;
        public ButtonSpriteSwapper MusicIconSwapper => musicIconSwapper;

        private LevelController _levelController;
        /// <summary>
        /// Панель із налаштуваннями
        /// </summary>
        public Canvas OptionsPanelCanvas => panelCanvas;

        /// <summary>
        /// <see cref="Button"/> який закриває <see cref="OptionsPanelCanvas"/>.
        /// </summary>
        public Button CloseOptionsButton => closePanelButton;

        /// <summary>
        /// Метод який закриває <see cref="OptionsPanelCanvas"/>.
        /// </summary>
        public override void Close()
        {
            if (_levelController.CurrentGameState == GameState.Menu)
            {
                OptionsPanelCanvas.enabled = false;
                menuCanvas.enabled = true;
                ScriptReferences.Instance.menuCarrousel.enabled = true;
            }
            else if (_levelController.CurrentGameState == GameState.PLaying)
            {
                OptionsPanelCanvas.enabled = false;
                levelUi.SetActive(true);
                Time.timeScale = 1f;
            }
            
        }

        /// <summary>
        /// Метод який відкриває <see cref="OptionsPanelCanvas"/>.
        /// </summary>
        public override void Open()
        {
            if (_levelController.CurrentGameState == GameState.Menu)
            {
                menuCanvas.enabled = false;
                OptionsPanelCanvas.enabled = true;
                ScriptReferences.Instance.menuCarrousel.enabled = false;
            }
            else if (_levelController.CurrentGameState == GameState.PLaying)
            {
                levelUi.SetActive(false);
                OptionsPanelCanvas.enabled = true;
                Time.timeScale = 0f;
            }

        }

        private void Start()
        {
           Subscribe();
           CloseOptionsButton.onClick.AddListener(Close);
        }

        public void Unsubscribe()
        {
            _levelController = null;
            menuCanvas = null;
            levelUi = null;
        }

        public void Subscribe()
        {
            _levelController ??= ScriptReferences.Instance.levelController;
            menuCanvas ??= ScriptReferences.Instance.menuUiHandler.MenuPanel;
            levelUi ??= _levelController.LevelUI;
        }
    }
}
