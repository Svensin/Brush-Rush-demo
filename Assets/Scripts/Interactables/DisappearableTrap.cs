using UnityEngine;

namespace Interactables
{
    public abstract class DisappearableTrap : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// ������� ������ 
        /// </summary>
        protected Animator trapAnimator;
        /// <summary>
        /// ������ ��'����, ��� � ���� ������� ��������� ��������������
        /// </summary>
        [SerializeField] protected GameObject trapModel;

        /// <summary>
        /// �����, �� ������� ������� ���������
        /// </summary>
        public abstract void Disable();

        /// <summary>
        /// �����, �� ������ ������ ������
        /// </summary>
        public abstract void Disappear();

        /// <summary>
        /// ��������� IInterectable ��� ������
        /// </summary>
        public abstract void Effect();
    }
}