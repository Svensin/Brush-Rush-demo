using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utilities.SaveLoadData;
using Utilities.SaversLoaders;

namespace Utilities
{
    /// <summary>
    /// Saves and loads data from files
    /// </summary>
    public class SaveLoadSystem : SingletonComponent<SaveLoadSystem>
    {
        /// <summary>
        /// Name of the directory where files are kept
        /// </summary>
        private const string DIRECTORY_NAME = "Files";

        /// <summary>
        /// Name of the gallery file
        /// </summary>
        private const string GALLERY_FILE = "gallery.dat";

        /// <summary>
        /// Name of the filters file
        /// </summary>
        private const string FILTERS_FILE = "filters.dat";

        private bool isPaused = false;

        private IEnumerable<ISaveLoad> _saveables;

        /// <summary>
        /// Creates dependencies of all classes which implement <see cref="ISaveLoad"/>.
        /// </summary>
        /// <param name="saveables">enumarates of all classes which implement <see cref="ISaveLoad"/></param>
        private void Construct(IEnumerable<ISaveLoad> saveables)
        {
            _saveables = saveables;
        }

        /// <summary>
        /// Initializes and combines directory path <see cref="DIRECTORY_NAME"/> in folder <see cref="Application.persistentDataPath"/>
        /// </summary>
        /// <returns>Path</returns>
        private string InitDataPath()
        {
            var directory = Path.Combine(Application.persistentDataPath, DIRECTORY_NAME);
            return DataPath = DataPath == Path.Combine(Application.persistentDataPath, DIRECTORY_NAME)
                ? DataPath
                : directory;
        }

        public string GalleryFileName => GALLERY_FILE;
        
         public string FiltersFileName => FILTERS_FILE;


        public string DataPath { get; private set; }

        void Awake()
        {
            DontDestroyOnLoad(this);
            Construct(new List<ISaveLoad>()
            {
                new PaintingSaverLoader(),
                new GallerySaverLoader(),
                new LevelNumberSaverLoader(),
                new OptionsSaverLoader(),
                new AudioSaverLoader()
            });

            DataPath = InitDataPath();
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
                SaveData();
            }
            else
            {
                LoadData();
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    isPaused = true;
                }
                else
                {
                    Application.Quit();
                }
            }
        }


        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveData();
            }
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        /// <summary>
        /// Loads data from all classes which implement <see cref="ISaveLoad"/>.
        /// </summary>
        public void LoadData()
        {
            foreach (ISaveLoad saveable in _saveables)
            {
                saveable.Load();
            }
        }

        /// <summary>
        /// Saves data from all classes which implement <see cref="ISaveLoad"/>.
        /// </summary>
        public void SaveData()
        {
            foreach (ISaveLoad saveable in _saveables)
            {
                saveable.Save();
            }
        }
    }
}