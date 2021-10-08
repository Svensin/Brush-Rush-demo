namespace Utilities.SaveLoadData
{
    /// <summary>
    /// Інтерфейс для збереження даних. 
    /// </summary>
    public interface ISaveLoad
    {
        /// <summary>
        /// Зберігає необхідні дані для класу. Дивитись метод <see cref="SaveLoadSystem.SaveData"/> класу <see cref="SaveLoadSystem"/> де відбувається виклик усіх збережень.
        /// </summary>
        /// <example>
        /// Цей код показує приклад реалізації <see cref="Save"/>.
        /// <code>
        /// public void Save()
        /// {
        ///    PlayerPrefs.SetString("test_name", "test data");
        /// }
        /// </code>
        /// </example>
        void Save();

        /// <summary>
        /// Завантажує необхідні дані для класу. Дивитись метод <see cref="SaveLoadSystem.LoadData"/> класу <see cref="SaveLoadSystem"/> де відбувається виклик усіх завантажень
        /// </summary>
        /// <example>
        /// Цей код показує приклад реалізації <see cref="Load"/>.
        /// <code>
        /// public void Load()
        /// {
        ///    var name = PlayerPrefs.GetString("test_name");
        /// }
        /// </code>
        /// </example>
        void Load();
    }
}