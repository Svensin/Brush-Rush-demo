using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// ����� ������ ������ ������
    /// </summary>
    public class HangingTrapEffect : DisappearableTrap
    {
        /// <summary>
        /// ��������� �� InkController
        /// </summary>
        InkController inkController;

        /// <summary>
        /// ������ ������� �������� ���� � ������ �������
        /// </summary>
        [Range(0f, 100f)] [SerializeField] float sandPaperDamagePercent;

        /// <summary>
        /// ��������� DisappearableTrap
        /// </summary>
        public override void Disappear()
        {
            trapModel.SetActive(false);
        }

        /// <summary>
        /// ��������� IInterectable
        /// </summary>
        public override void Effect()
        {
            float percentToSet = inkController.CurrentInkCapacity / inkController.MaxInkCapacity - sandPaperDamagePercent / 100f;

            inkController.SetCapacityPercentage(percentToSet, true);

            ScriptReferences.Instance.brushFlickeringEffect.StartOrContinueEffect();
        }

        /// <summary>
        /// ��������� IInterectable
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