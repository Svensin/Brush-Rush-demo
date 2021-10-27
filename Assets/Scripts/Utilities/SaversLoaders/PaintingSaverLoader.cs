using System.Collections.Generic;
using System.IO;
using System.Linq;
using GalleryLogic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaveLoadData;

namespace Utilities.SaversLoaders
{
    /// <summary>
    /// Saves and loads data of <see cref="Gallery"/>.
    /// </summary>
    public class PaintingSaverLoader : ISaveLoad
    {
        /// <summary>
        /// Save painting of <see cref="Gallery"/> in JSON-file.
        /// </summary>
        private void SavePaintings()
        {
            var paintingImages = Gallery.Instance.Paintings;
            var paintings = ProcessPaintingsData(paintingImages);

            string json = JsonUtilityArrayWrapper.ToJson(paintings);

            using StreamWriter file = File.CreateText(Path.Combine(SaveLoadSystem.Instance.DataPath, SaveLoadSystem.Instance.GalleryFileName));
            file.Write(json);
        }

        /// <summary>
        /// Loads painting of <see cref="Gallery"/> from JSON-file.
        /// </summary>
        private void LoadPaintings()
        {
            var paintingImages = Gallery.Instance.Paintings;
            using StreamReader file = File.OpenText(Path.Combine(SaveLoadSystem.Instance.DataPath, SaveLoadSystem.Instance.GalleryFileName));
            var json = file.ReadToEnd();

            var paintings = JsonUtilityArrayWrapper.FromJson<Painting>(json);

            UpdatePaintingsData(paintingImages, paintings);
        }


        /// <summary>
        /// Update data from <see cref="Painting"/> in <see cref="GameObject"/>.
        /// </summary>
        /// <param name="images">list of game objects-paintings</param>
        /// <param name="paintings">list of paintings data</param>
        private static void UpdatePaintingsData(List<Image> images, Painting[] paintings)
        {
            for (int i = 0; i < images.Count; i++)
            {
                Painting.UpdateImagePieces(paintings[i], images[i].gameObject);
            }
        }


        /// <summary>
        /// Processes and load from game objects-paintigs
        /// </summary>
        /// <param name="paintingImages">list of game objects-paintings</param>
        /// <returns>array of <see cref="Painting"/> for saving</returns>
        private static Painting[] ProcessPaintingsData(List<Image> paintingImages)
        {
            Painting[] paintings = new Painting[paintingImages.Count];

            for (int i = 0; i < paintingImages.Count; i++)
            {
                var scriptableObject = ScriptReferences.Instance.Locator.PaintingSpritesList.FirstOrDefault(p =>
                    p.name == paintingImages[i].gameObject.name);
                
                //створюємо модель картини
                paintings[i] = new Painting
                {
                    name = paintingImages[i].gameObject.name,
                    pieces = new Piece[paintingImages[i].gameObject.transform.childCount]
                };

                if (scriptableObject != null)
                {
                    paintings[i].colorized = paintingImages[i].sprite == scriptableObject.FirstSpriteToSwap;
                }

                for (int j = 0; j < paintingImages[i].gameObject.transform.childCount; j++)
                {
                    var pieceGameObject = paintingImages[i].gameObject.transform.GetChild(j).gameObject;
                    paintings[i].pieces[j] = new Piece
                    {
                        id = pieceGameObject.name, isEnabled = pieceGameObject.activeSelf
                    };
                }
            }

            return paintings;
        }

        public void Save()
        {
            SavePaintings();
        }

        public void Load()
        {
            LoadPaintings();
        }
    }
}