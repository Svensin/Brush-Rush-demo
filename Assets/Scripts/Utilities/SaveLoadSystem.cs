using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utilities.SaveLoadData;
using Utilities.SaversLoaders;

namespace Utilities
{
    /// <summary>
    /// Зберігає та завантажує дані з файлів
    /// </summary>
    public class SaveLoadSystem : SingletonComponent<SaveLoadSystem>
    {
        /// <summary>
        /// Назва директорії із файлами
        /// </summary>
        private const string DIRECTORY_NAME = "Files";

        /// <summary>
        /// Назва файлу для галереї
        /// </summary>
        private const string GALLERY_FILE = "gallery.dat";
        
        /// <summary>
        /// Назва файлу для фільтрів
        /// </summary>
        private const string FILTERS_FILE = "filters.dat";

        private bool isPaused = false;

        private IEnumerable<ISaveLoad> _saveables;

        /// <summary>
        /// Впроваджує залежності усіх класів, що реалізують <see cref="ISaveLoad"/>.
        /// </summary>
        /// <param name="saveables">перечислення усіх класів, що реалізують <see cref="ISaveLoad"/></param>
        private void Construct(IEnumerable<ISaveLoad> saveables)
        {
            _saveables = saveables;
        }

        /// <summary>
        /// Ініціалізує та комбінує шлях директорії <see cref="DIRECTORY_NAME"/> у папці <see cref="Application.persistentDataPath"/>
        /// </summary>
        /// <returns>Повертає шлях</returns>
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
        /// Завантажує дані з усіх класів, що реалізують <see cref="ISaveLoad"/>.
        /// </summary>
        public void LoadData()
        {
            foreach (ISaveLoad saveable in _saveables)
            {
                saveable.Load();
            }
        }

        /// <summary>
        /// Зберігає дані усіх класів, що реалізують <see cref="ISaveLoad"/>.
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