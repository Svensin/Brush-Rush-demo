using System;

namespace Utilities.SaveLoadData
{
    /// <summary>
    /// Sereilized model of painting piece for saving/loading. Is part of <see cref="Painting"/>.
    /// </summary>
    [Serializable]
    public class Piece
    {
        /// <summary>
        /// ID of painting piece
        /// </summary>
        public string id;
        /// <summary>
        /// is piece active
        /// </summary>
        public bool isEnabled;
    }
}