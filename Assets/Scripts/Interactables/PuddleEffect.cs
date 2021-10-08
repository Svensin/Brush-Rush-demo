using UnityEngine;

namespace Interactables
{
    public class PuddleEffect : MonoBehaviour, IInteractable
    {

        [Min(0)][SerializeField] float inkUsePerSecMultiplier;


        public void Disable()
        {
            ScriptReferences.Instance.inkControllerScript.SetInitialInkUsagePerSec(this);
        }

        public void Effect()
        {
            ScriptReferences.Instance.inkControllerScript.MultipyInkUsagePerSec(this, inkUsePerSecMultiplier);
        }


        private void OnTriggerStay(Collider other)
        {
            ScriptReferences.Instance.inkControllerScript.DecreaseCapacityPerSec();
        }

        private void OnTriggerEnter(Collider other)
        {
            Effect();
        }

        private void OnTriggerExit(Collider other)
        {
            Disable();
        }


    }

}

