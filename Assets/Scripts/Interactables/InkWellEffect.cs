using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// Логіка ефекту чорнильниці
    /// </summary>
    public class InkWellEffect : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Посилання на InkController
        /// </summary>
        InkController inkController;
        /// <summary>
        /// Булева змінна, що відповідає за перевірку того, чи використана чорнильния
        /// </summary>
        private bool isRefilled = false;

        /// <summary>
        /// Реалізація IInterectable
        /// </summary>
        public void Disable()
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// Реалізація IInterectable
        /// </summary>
        public void Effect()
        {
            if (!isRefilled)
            {
                inkController.SetCapacityPercentage(1);
                isRefilled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Brush"))
            {
                if (inkController == null)
                {
                    inkController = other.transform.parent.GetComponent<InkController>();
                }
                
                Effect();
                Disable();
            }
        }
    }
}