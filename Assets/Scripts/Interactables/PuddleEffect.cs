using UnityEngine;

namespace Interactables
{
    public class PuddleEffect : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// how much usage of ink usage while brush interacts with paddle is increased
        /// </summary>
        [Min(0)][SerializeField] float inkUsePerSecMultiplier;

        /// <summary>
        /// sets ink usage usage to initial value
        /// </summary>
        public void Disable()
        {
            ScriptReferences.Instance.inkControllerScript.SetInitialInkUsagePerSec(this);
        }

        /// <summary>
        /// increases ink usage by <see cref="inkUsePerSecMultiplier"/>
        /// </summary>
        public void Effect()
        {
            ScriptReferences.Instance.inkControllerScript.MultipyInkUsagePerSec(this, inkUsePerSecMultiplier);
        }

        /// <summary>
        /// Decreases ink capacity
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            ScriptReferences.Instance.inkControllerScript.DecreaseCapacityPerSec();
        }

        /// <summary>
        /// Starts paddle effect
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            Effect();
        }

        /// <summary>
        /// Disables trap effect
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            Disable();
        }


    }

}

