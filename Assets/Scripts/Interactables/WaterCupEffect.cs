using UnityEngine;

namespace Interactables
{
    public class WaterCupEffect : DisappearableTrap
    {
        /// <summary>
        /// reference to InkController
        /// </summary>
        InkController inkController;

        /// <summary>
        /// Disables model
        /// </summary>
        public override void Disappear()
        {
            trapModel.SetActive(false);
        }

        /// <summary>
        /// Emptys all ink capacity
        /// </summary>
        public override void Effect()
        {
            inkController.SetCapacityPercentage(0);
        }

        /// <summary>
        /// Disables collider and starts dissapear animation
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

        /// <summary>
        /// Starts effect and disables a trap
        /// </summary>
        /// <param name="other"></param>
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