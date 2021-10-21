using UnityEngine;
using Utilities.SaveLoadData;

namespace Utilities.SaversLoaders
{
    /// <summary>
    /// Saves data of levels<see cref="Gallery"/>.
    /// </summary>
    public class LevelNumberSaverLoader : ISaveLoad
    {
        public void Save()
        {
            PlayerPrefs.SetInt(PlayerPrefsVariables.CURRENT_LEVEL, LevelController.CurrentLevel);
        }

        public void Load()
        {
            LevelController.CurrentLevel = PlayerPrefs.GetInt(PlayerPrefsVariables.CURRENT_LEVEL, 0);
        }
    }
}