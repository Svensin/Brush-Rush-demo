using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class HangingTrapEffect : DisappearableTrap
    {
        /// <summary>
        /// Reference to InkController
        /// </summary>
        InkController inkController;

        /// <summary>
        /// How many percentage of maximum value trap deals damage to ink capacity
        /// </summary>
        [Range(0f, 100f)] [SerializeField] float sandPaperDamagePercent;

        /// <summary>
        /// Disables trap model
        /// </summary>
        public override void Disappear()
        {
            trapModel.SetActive(false);
        }

        /// <summary>
        /// Deals certain damage to ink capacity
        /// </summary>
        public override void Effect()
        {
            float percentToSet = inkController.CurrentInkCapacity / inkController.MaxInkCapacity - sandPaperDamagePercent / 100f;

            inkController.SetCapacityPercentage(percentToSet, true);

            ScriptReferences.Instance.brushFlickeringEffect.StartOrContinueEffect();
        }

        /// <summary>
        /// Starts trap disappear animation and disables it`s collider
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