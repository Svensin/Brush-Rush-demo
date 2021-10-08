using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilities.SaversLoaders;

namespace GalleryLogic
{
    /// <summary>
    /// Галерея картин.
    /// </summary>
    public class Gallery : SingletonComponent<Gallery>
    {
        /// <summary>
        /// UI для <see cref="Gallery"/>.
        /// </summary>
        [SerializeField] private GalleryUi galleryUi;
        /// <summary>
        /// UI для <see cref="Gallery"/>.
        /// </summary>
        public GalleryUi GalleryUi => galleryUi;

        [SerializeField] private List<Image> paintings;
        [SerializeField] private List<GameObject> filters;
    
        /// <summary>
        /// Список картин.
        /// </summary>
        public List<Image> Paintings => paintings;

        /// <summary>
        /// Список фільтрів на картини
        /// </summary>
        public List<GameObject> Filters => filters;
    
        [SerializeField] private Image currentPainting;
        private GameObject _currentFilter;

        /// <summary>
        /// Поточна картина.
        /// </summary>
        public Image CurrentPainting => currentPainting;

        /// <summary>
        /// Призначає поточну <see cref="CurrentPainting"/>.
        /// </summary>
        /// <param name="sender">ініціатор виклику</param>
        /// <param name="newCurrentPainting">нова поточна картина</param>
        public void SetCurrentPainting(object sender, Image newCurrentPainting)
        {
            if (sender is GallerySaverLoader)
            {
                if (newCurrentPainting != null)
                {
                    currentPainting = newCurrentPainting;
                    _currentFilter = filters.FirstOrDefault(g => 
                        g.transform.parent.Equals(currentPainting.transform.parent));
                }
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Змінює <see cref="CurrentPainting"/>.
        /// </summary>
        private void ChangeCurrentPainting()
        {
            if (paintings.IndexOf(currentPainting) < paintings.Count - 1)
            {
                currentPainting = paintings[paintings.IndexOf(currentPainting) + 1];
                _currentFilter = filters[filters.IndexOf(_currentFilter) + 1];
            }
            else
            {
                Debug.Log("No more paintings available! ");
            }
        
        }

        /// <summary>
        /// Оновлює <see cref="CurrentPainting"/> із дубліката.
        /// </summary>
        /// <param name="duplicatePainting">дублікат картини</param>
        public void UpdateCurrentPainting(GameObject duplicatePainting)
        {
            List<Transform> children = new List<Transform>();
            for (int i = 0; i < currentPainting.transform.childCount; i++)
            {
                currentPainting.transform.GetChild(i)
                    .gameObject.SetActive(duplicatePainting
                        .transform.GetChild(i).gameObject.activeSelf);
            
                children.Add(currentPainting.transform.GetChild(i));
            }
        
            var activeCount = children.Count(t => t.gameObject.activeSelf);

            if (activeCount == 0)
            {
                currentPainting.gameObject.SetActive(true);
                _currentFilter.SetActive(false);
                ChangeCurrentPainting();
            }
        }
    }
}
