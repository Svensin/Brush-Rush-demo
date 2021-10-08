using UnityEngine;

namespace Interactables
{
    public class WaterCupEffect : DisappearableTrap
    {
        /// <summary>
        /// Посилання на InkController
        /// </summary>
        InkController inkController;

        /// <summary>
        /// Реалізація DisappearableTrap
        /// </summary>
        public override void Disappear()
        {
            trapModel.SetActive(false);
        }

        /// <summary>
        /// Реалізація IInteractable
        /// </summary>
        public override void Effect()
        {
            inkController.SetCapacityPercentage(0);
        }

        /// <summary>
        /// Реалізація IInteractable
        /// </summary>
        public override void Disable()
        {
            Collider waterCupCollider = GetComponent<Collider>();
            waterCupCollider.enabled = false;

            trapAnimator.SetTrigger("Disappear");
        }

        private void Start()
        {
            trapAnimator = GetComponent<Animator>();
        }

        private void OnTriggerEnter(Collider other)
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