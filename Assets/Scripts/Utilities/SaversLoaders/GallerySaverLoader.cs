using System.Collections.Generic;
using System.IO;
using System.Linq;
using GalleryLogic;
using UnityEngine;
using Utilities.SaveLoadData;

namespace Utilities.SaversLoaders
{
    /// <summary>
    /// Saves data of <see cref="Gallery"/>.
    /// </summary>
    public class GallerySaverLoader : ISaveLoad
    {
        public void Save()
        {
            SaveCurrentPainting();
            SaveFilters();
        }


        public void Load()
        {
            LoadCurrentPainting();
            LoadFilters();
        }

        #region Filters

        private void SaveFilters()
        {
            var filterGameObjects = Gallery.Instance.Filters;
            var filters = ProcessFilters(filterGameObjects);

            string json = JsonUtilityArrayWrapper.ToJson(filters);

            using StreamWriter file = File.CreateText(Path.Combine(SaveLoadSystem.Instance.DataPath,
                SaveLoadSystem.Instance.FiltersFileName));
            file.Write(json);
        }

        private void LoadFilters()
        {
            var filtersGameObjects = Gallery.Instance.Filters;
            using StreamReader file = File.OpenText(Path.Combine(SaveLoadSystem.Instance.DataPath,
                SaveLoadSystem.Instance.FiltersFileName));
            var json = file.ReadToEnd();

            var filters = JsonUtilityArrayWrapper.FromJson<Filter>(json);

            for (int i = 0; i < filtersGameObjects.Count; i++)
            {
                Filter.UpdateFilter(filters[i], filtersGameObjects[i]);
            }
        }

        private Filter[] ProcessFilters(IReadOnlyList<GameObject> filters)
        {
            var filtersData = new Filter[filters.Count];

            for (int i = 0; i < filters.Count; i++)
            {
                filtersData[i] = new Filter {isEnabled = filters[i].activeSelf};
            }

            return filtersData;
        }

        #endregion

        #region CurrentPainting

        private void SaveCurrentPainting()
        {
            PlayerPrefs.SetString(PlayerPrefsVariables.CURRENT_PAINTING_NAME, Gallery.Instance.CurrentPainting.name);
        }

        private void LoadCurrentPainting()
        {
            var name = PlayerPrefs.GetString(PlayerPrefsVariables.CURRENT_PAINTING_NAME);
            var current = Gallery.Instance.Paintings.FirstOrDefault(p => p.name.Equals(name));
            Gallery.Instance.SetCurrentPainting(this, current);
        }

        #endregion
    }
}