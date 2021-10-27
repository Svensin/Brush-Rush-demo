using UnityEngine;

namespace Interactables
{
    public class HoleEffect : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// reference to brush gameobject
        /// </summary>
        private GameObject _brush;
        /// <summary>
        /// reference to LevelLogic
        /// </summary>
        private LevelLogic.LevelMovement _level;
        /// <summary>
        /// reference to collider of the hole
        /// </summary>
        private Collider _collider;
        /// <summary>
        /// acceleration of brush then it falls into hole
        /// </summary>
        [SerializeField] private float _acceleration;

        /// <summary>
        /// Stops level movement and disables it`s collider
        /// </summary>
        public void Disable()
        {
            _collider.enabled = false;
            ScriptReferences.Instance.levelController.OnLoss();
        }

        /// <summary>
        /// Makes brush fall down
        /// </summary>
        public void Effect()
        {

            var rigidBody = _brush.GetComponent<Rigidbody>();
            rigidBody.angularVelocity = Vector3.right * 15 * Mathf.Deg2Rad;
            rigidBody.isKinematic = false;
            rigidBody.AddForce(Vector3.down * _acceleration, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Brush"))
            {
                _brush = other.gameObject;

                Effect();
                Disable();
            }
        }

        private void Start()
        {
            _collider = GetComponent<Collider>();
            _level = ScriptReferences.Instance.levelScript;
        }
    }
}

