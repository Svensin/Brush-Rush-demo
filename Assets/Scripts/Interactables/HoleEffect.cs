using UnityEngine;

namespace Interactables
{
    public class HoleEffect : MonoBehaviour, IInteractable
    {
        /// <summary>
        /// Посилання на пензлик
        /// </summary>
        private GameObject _brush;
        /// <summary>
        /// Посилання на LevelLogic
        /// </summary>
        private LevelLogic.Level _level;
        /// <summary>
        /// Посилання на колайдер провалини
        /// </summary>
        private Collider _collider;
        /// <summary>
        /// Прискорення, що надається пензлику при падінні в провалину
        /// </summary>
        [SerializeField] private float _acceleration;

        /// <summary>
        /// Реалізація IInteractable
        /// </summary>
        public void Disable()
        {
            _collider.enabled = false;
            ScriptReferences.Instance.levelController.OnLoss();
        }

        /// <summary>
        /// Реалізація IInteractable
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

