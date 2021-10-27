using System.Collections;
using GalleryLogic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UiLogic
{
    /// <summary>
    /// Conrtrols disappearence of <see cref="Scrollbar"/> in <see cref="Gallery.GalleryPanel"/>.
    /// </summary>
    public class GalleryScrollBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// Background image of <see cref="Scrollbar"/>>.
        /// </summary>
        [SerializeField] private Image scrollBarBackground;
        /// <summary>
        /// Handle image of <see cref="Scrollbar"/>>.
        /// </summary>
        [SerializeField] private Image scrollBarHandle;

        private Coroutine fade;

        private bool _pressed = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;
            ShowScrollBar();
        }

        /// <summary>
        /// Makes <see cref="Scrollbar"/ appear>.
        /// </summary>
        private void ShowScrollBar()
        {
            if (fade != null)
            {
                StopCoroutine(fade);
            }
          
            scrollBarBackground.color = new Color(scrollBarBackground.color.r, scrollBarBackground.color.g,
                scrollBarBackground.color.b, 0.33f);
            scrollBarHandle.color =
                new Color(scrollBarHandle.color.r, scrollBarHandle.color.g, scrollBarHandle.color.b, 1);
            _pressed = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            fade = StartCoroutine(Fade());
        }

        /// <summary>
        /// <see cref="Coroutine"/> which makes <see cref="Scrollbar"/ disappear>.
        /// </summary>
        /// <returns></returns>
        IEnumerator Fade()
        {
            var t = 0f;
            var backColor = scrollBarBackground.color;
            var handleColor = scrollBarHandle.color;
            while (backColor.a > 0 && handleColor.a > 0)
            {
                t += Time.deltaTime;
                backColor.a = Mathf.Lerp(1, 0, t);
                handleColor.a = Mathf.Lerp(1, 0, t);
                
                scrollBarBackground.color = backColor;
                scrollBarHandle.color = handleColor;
                
                yield return new WaitForSeconds(0.01f);
                
                if (_pressed)
                {
                    ShowScrollBar();
                }

            }
        }
    }
}