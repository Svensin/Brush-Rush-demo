using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class SandPaperEffect : DisappearableTrap
    {
        /// <summary>
        /// reference to InkController
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
        /// Deals damage
        /// </summary>
        public override void Effect()
        {
            float percentToSet = inkController.CurrentInkCapacity / inkController.MaxInkCapacity - sandPaperDamagePercent / 100f;

            //float percentToSet = (inkController.CurrentInkCapacity / inkController.MaxInkCapacity) * percentToMultiply;


            inkController.SetCapacityPercentage(percentToSet,true);
            ScriptReferences.Instance.brushFlickeringEffect.StartOrContinueEffect();
        }

        /// <summary>
        ///Disables collider and starts dissapear animation
        /// </summary>
        public override void Disable()
        {
            Collider sandPaperCollider = GetComponent<Collider>();
            sandPaperCollider.enabled = false;

            trapAnimator.SetTrigger("Disappear");
        }

        private void Start()
        {
            trapAnimator = GetComponent<Animator>();
        }
        /// <summary>
        /// Starts effect and disables trap
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
