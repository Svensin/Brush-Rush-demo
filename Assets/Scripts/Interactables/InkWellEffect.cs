using UnityEngine;

namespace Interactables
{
    public class InkWellEffect : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// reference InkController
        /// </summary>
        InkController inkController;
        /// <summary>
        /// checks if brush has already interacted with ink well
        /// </summary>
        private bool isRefilled = false;

        /// <summary>
        /// Disabes ink well collider
        /// </summary>
        public void Disable()
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// Fully refills ink capacity
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