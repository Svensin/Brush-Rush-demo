using System;

namespace Utilities.SaveLoadData
{
    /// <summary>
    /// Серіалізована модель частини полотна для збереження/завантаження, що закриває картину. Є частиною <see cref="Painting"/>.
    /// </summary>
    [Serializable]
    public class Piece
    {
        /// <summary>
        /// Ідентифікатор частинки
        /// </summary>
        public string id;
        /// <summary>
        /// Чи активна частинка полотна
        /// </summary>
        public bool isEnabled;
    }
}