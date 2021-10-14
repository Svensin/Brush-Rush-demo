using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LevelLogic;
using UnityEngine.Serialization;

namespace LevelLogic
{
    /// <summary>
    /// Describes logic of level objects
    /// </summary>
    public class LevelObject : MonoBehaviour, IDisposable, IProducible
    {
        /// <summary>
        /// 3d model of object
        /// </summary>
        [FormerlySerializedAs("TrapModel")] 
        [SerializeField] private GameObject ObjectModel;

        /// <summary>
        /// Deactivates object
        /// </summary>
        public void Dispose()
        {
            ObjectModel.SetActive(false);
        }

        /// <summary>
        /// Asctivates object
        /// </summary>
        public void Produce()
        {
            ObjectModel.SetActive(true);
        }
    }
}


