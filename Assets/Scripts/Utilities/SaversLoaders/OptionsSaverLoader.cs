using Utilities.SaveLoadData;
using OptionsLogic;
using UnityEngine;

namespace Utilities.SaversLoaders
{
    /// <summary>
    /// Saves data of <see cref="Options"/>.
    /// </summary>
    public class OptionsSaverLoader : ISaveLoad
    {
        public void Save()
        {
            var musicActive = Options.Instance.OptionsUi.MusicIconSwapper.IconActive ? 1 : 0;
            var soundsActive = Options.Instance.OptionsUi.SoundsIconSwapper.IconActive ? 1 : 0;
            var vibrationActive = Options.Instance.OptionsUi.VibrationsIconSwapper.IconActive ? 1 : 0;
            PlayerPrefs.SetInt(PlayerPrefsVariables.MUSIC,  musicActive);
            PlayerPrefs.SetInt(PlayerPrefsVariables.SOUNDS,  soundsActive);
            PlayerPrefs.SetInt(PlayerPrefsVariables.VIBRATIONS, vibrationActive);
        }

        public void Load()
        {
            var musicActive = PlayerPrefs.GetInt(PlayerPrefsVariables.MUSIC, 1) == 1;
            var soundsActive = PlayerPrefs.GetInt(PlayerPrefsVariables.SOUNDS, 1) == 1;
            var vibrationsActive = PlayerPrefs.GetInt(PlayerPrefsVariables.VIBRATIONS, 1) == 1;
            Options.Instance.OptionsUi.MusicIconSwapper.Swap(musicActive);
            Options.Instance.OptionsUi.SoundsIconSwapper.Swap(soundsActive);
            Options.Instance.OptionsUi.VibrationsIconSwapper.Swap(vibrationsActive);
           
        }
    }
}