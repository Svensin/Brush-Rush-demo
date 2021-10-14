using UnityEngine;
using LevelLogic;
using System;

namespace LevelLogic
{
    /// <summary>
    /// Deactivates object then they are out of sight
    /// </summary>
    public class Absorber : MonoBehaviour
    {
        /// <summary>
        /// Deactivates level object
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            IDisposable objectDispose = other.transform.parent.GetComponent<IDisposable>();

            objectDispose.Dispose();

        }
    }
}