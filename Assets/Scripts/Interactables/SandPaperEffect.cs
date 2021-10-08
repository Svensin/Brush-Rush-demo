using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// Логіка ефекту наждачного паперу
    /// </summary>
    public class SandPaperEffect : DisappearableTrap
    {
        /// <summary>
        /// Посилання на InkController
        /// </summary>
        InkController inkController;

        /// <summary>
        /// Скільки відсотків наждачка зніме з запасу чорнила
        /// </summary>
        [Range(0f, 100f)] [SerializeField] float sandPaperDamagePercent;

        /// <summary>
        /// Реалізація DisappearableTrap
        /// </summary>
        public override void Disappear()
        {
            trapModel.SetActive(false);
        }

        /// <summary>
        /// Реалізація IInterectable
        /// </summary>
        public override void Effect()
        {
            float percentToSet = inkController.CurrentInkCapacity / inkController.MaxInkCapacity - sandPaperDamagePercent / 100f;

            //float percentToSet = (inkController.CurrentInkCapacity / inkController.MaxInkCapacity) * percentToMultiply;


            inkController.SetCapacityPercentage(percentToSet,true);
            ScriptReferences.Instance.brushFlickeringEffect.StartEffect();
        }

        /// <summary>
        /// Реалізація IInterectable
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
