using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.SaveLoadData
{
    /// <summary>
    /// Sereilized model of painting for saving/loading in/from <see cref="SaversLoaders.PaintingSaverLoader"/>.
    /// </summary>
    [Serializable]
    public class Painting
    {
        /// <summary>
        /// Id of painting
        /// </summary>
        public string name;
        /// <summary>
        /// Array of painting pieces<see cref="Piece"/>.
        /// </summary>
        public Piece[] pieces;

        public bool colorized;

        /// <summary>
        /// Update game object <see cref="GameObject"/> from model <see cref="Painting"/>.
        /// </summary>
        /// <param name="paintingData">model-painting</param>
        /// <param name="gameObject">game object-painting</param>
        /// <returns>changed object(optional)</returns>
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