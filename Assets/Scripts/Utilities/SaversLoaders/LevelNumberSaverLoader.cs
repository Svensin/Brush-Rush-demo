using UnityEngine;
using Utilities.SaveLoadData;

namespace Utilities.SaversLoaders
{
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