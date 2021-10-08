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
    /// Зберігає та завантажує дані, пов'язані із <see cref="Painting"/> та <see cref="Gallery"/>
    /// </summary>
    public class PaintingSaverLoader : ISaveLoad
    {
        /// <summary>
        /// Зберігає картини з <see cref="Gallery"/> у JSON-файл.
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
        /// Завантажує картини у <see cref="Gallery"/> з JSON-файлу.
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
        /// Оновлює дані картин з <see cref="Painting"/> у <see cref="GameObject"/>.
        /// </summary>
        /// <param name="images">список ігрових об'єктів-картин</param>
        /// <param name="paintings">список картин-даних</param>
        private static void UpdatePaintingsData(List<Image> images, Painting[] paintings)
        {
            for (int i = 0; i < images.Count; i++)
            {
                Painting.UpdateImagePieces(paintings[i], images[i].gameObject);
            }
        }
        
       
        /// <summary>
        /// Оброблює та витягує з ігрових об'єктів-картин дані
        /// </summary>
        /// <param name="paintingImages">список ігрових об'єктів-картин</param>
        /// <returns>масив <see cref="Painting"/> для збереження</returns>
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