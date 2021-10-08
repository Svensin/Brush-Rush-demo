using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelLogic
{
    /// <summary>
    /// Контролює переміщення рівня
    /// </summary>
    public class Level : MonoBehaviour
    {
        [Header("Blocks data")][Space]
        [SerializeField] public List<BlockData> BlocksData;

        /// <summary>
        /// Швидкість переміщення рівня
        /// </summary>
        [Header("Level props")][Space]
        [Range(1,15)][SerializeField] private int _velocity;
        public float Velocity => _velocity;

        [Header("Line containers")] [Space] [SerializeField]
        private GameObject paperLinesContainer;

        public GameObject PaperLinesContainer => paperLinesContainer;
        
        [SerializeField]
        private GameObject badLinesContainer;

        public GameObject BadLinesContainer => badLinesContainer;
        
        /// <summary>
        /// Початкова позиція
        /// </summary>
        private float _initialZValue;
        /// <summary>
        /// Фізичне тіло рівня
        /// </summary>
        private Rigidbody _levelRigidBody;

        private bool isMoving = false;
        
        private void Awake()
        {
            _levelRigidBody = GetComponent<Rigidbody>();
            _initialZValue = transform.position.z;
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                Move();
            }
        }

        /// <summary>
        /// Рухає рівень
        /// </summary>
        public void Move()
        {
            var prevPos = transform.position;
            var newPos = new Vector3(prevPos.x, prevPos.y, prevPos.z - _velocity * Time.fixedDeltaTime);
            _levelRigidBody.MovePosition(newPos);
        }

        /// <summary>
        /// Зупиняє рівень
        /// </summary>
        public void StopLevelMovement()
        {
            isMoving = false;
        }

        public void StartLevelMovement()
        {
            isMoving = true;
        }
    }

}
