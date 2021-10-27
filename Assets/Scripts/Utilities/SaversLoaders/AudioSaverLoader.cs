using UnityEngine;
using Utilities.SaveLoadData;

namespace Utilities.SaversLoaders
{
    /// <summary>
    /// Saves data of audio.
    /// </summary>
    public class AudioSaverLoader : ISaveLoad
    {
        public void Save()
        {
        }

        public void Load()
        {
            var musicActive = PlayerPrefs.GetInt(PlayerPrefsVariables.MUSIC, 1) == 1;
            var soundsActive = PlayerPrefs.GetInt(PlayerPrefsVariables.SOUNDS, 1) == 1;
            AudioManager.Instance.SetMusic(this, musicActive);
            AudioManager.Instance.SetSounds(this, soundsActive);
        }
    }
}