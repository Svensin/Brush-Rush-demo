using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// ����� ������ �����������
    /// </summary>
    public class InkWellEffect : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// ��������� �� InkController
        /// </summary>
        InkController inkController;
        /// <summary>
        /// ������ �����, �� ������� �� �������� ����, �� ����������� ����������
        /// </summary>
        private bool isRefilled = false;

        /// <summary>
        /// ��������� IInterectable
        /// </summary>
        public void Disable()
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }

        /// <summary>
        /// ��������� IInterectable
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