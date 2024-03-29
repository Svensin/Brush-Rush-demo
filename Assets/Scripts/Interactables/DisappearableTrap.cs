using UnityEngine;

namespace Interactables
{
    public abstract class DisappearableTrap : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Reference to trapAnimator
        /// </summary>
        protected Animator trapAnimator;
        /// <summary>
        /// Reference to trap model
        /// </summary>
        [SerializeField] protected GameObject trapModel;

        /// <summary>
        /// Starts trap disappear animation and disables it`s collider
        /// </summary>
        public abstract void Disable();

        /// <summary>
        /// Disables trap model
        /// </summary>
        public abstract void Disappear();

        /// <summary>
        /// Activates trap effect
        /// </summary>
        public abstract void Effect();
    }
}