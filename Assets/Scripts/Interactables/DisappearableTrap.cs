using UnityEngine;

namespace Interactables
{
    public abstract class DisappearableTrap : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Аніматор пастки 
        /// </summary>
        protected Animator trapAnimator;
        /// <summary>
        /// Модель об'єкта, яка в кінці анімації зникнення деактивовується
        /// </summary>
        [SerializeField] protected GameObject trapModel;

        /// <summary>
        /// Метод, що викликає анімацію зникнення
        /// </summary>
        public abstract void Disable();

        /// <summary>
        /// Метод, що вимикає модель пастки
        /// </summary>
        public abstract void Disappear();

        /// <summary>
        /// Реалізація IInterectable для пастки
        /// </summary>
        public abstract void Effect();
    }
}