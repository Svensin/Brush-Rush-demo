using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelLogic
{
    /// <summary>
    /// Controls level movement
    /// </summary>
    public class LevelMovement : MonoBehaviour
    {
        [Header("Blocks data")][Space]
        [SerializeField] public List<BlockData> BlocksData;

        /// <summary>
        /// Velocity of level movement
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
        /// Initial  z value of position
        /// </summary>
        private float _initialZValue;
      
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
        /// Moves level, should be called once per frame
        /// </summary>
        public void Move()
        {
            var prevPos = transform.position;
            var newPos = new Vector3(prevPos.x, prevPos.y, prevPos.z - _velocity * Time.fixedDeltaTime);
            _levelRigidBody.MovePosition(newPos);
        }

        /// <summary>
        /// Stops level movement
        /// </summary>
        public void StopLevelMovement()
        {
            isMoving = false;
        }

        /// <summary>
        /// Starts level movement
        /// </summary>
        public void StartLevelMovement()
        {
            isMoving = true;
        }
    }

}
