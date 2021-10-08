using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.SaveLoadData
{
    /// <summary>
    /// Серіалізована модель картини для збереження/завантаження у <see cref="SaversLoaders.PaintingSaverLoader"/>.
    /// </summary>
    [Serializable]
    public class Painting
    {
        /// <summary>
        /// Ідентифікатор картини
        /// </summary>
        public string name;
        /// <summary>
        /// Масив частинок <see cref="Piece"/>.
        /// </summary>
        public Piece[] pieces;

        public bool colorized;

        /// <summary>
        /// Оновлює ігровий об'єкт <see cref="GameObject"/> із моделі <see cref="Painting"/>.
        /// </summary>
        /// <param name="paintingData">модель-картина</param>
        /// <param name="gameObject">ігровий об'єкт-картина</param>
        /// <returns>змінений об'єкт (optional)</returns>
        public static GameObject UpdateImagePieces(Painting paintingData, GameObject gameObject)
        {
            for (int j = 0; j < gameObject.transform.childCount; j++)
            {
                var pieceTransform = gameObject.transform.GetChild(j);
                var dataPiece = paintingData.pieces[j];
                pieceTransform.gameObject.SetActive(dataPiece.isEnabled);
            }
            
            var painting = gameObject.GetComponent<Image>();
            var paintingScriptable = ScriptReferences.Instance.Locator.PaintingSpritesList.FirstOrDefault(p =>
                p.name == paintingData.name);

            if (paintingScriptable != null)
            {
                painting.sprite = paintingData.colorized ? paintingScriptable.FirstSpriteToSwap : paintingScriptable.DefaultSprite;
            }
            else
            {
                Debug.LogError("There is no such scriptable object!!!");
            }


            return gameObject;
        }
    }
}