using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LevelLogic;
using UnityEngine.Serialization;

namespace LevelLogic
{
    /// <summary>
    /// Описує логіку перешкод
    /// </summary>
    public class LevelObject : MonoBehaviour, IDisposable, IProducible
    {
        /// <summary>
        /// Модель перешкоди
        /// </summary>
        [FormerlySerializedAs("TrapModel")] 
        [SerializeField] private GameObject ObjectModel;

        /// <summary>
        /// Вимикає перешкоду
        /// </summary>
        public void Dispose()
        {
            ObjectModel.SetActive(false);
        }

        /// <summary>
        /// Вмикає перешкоду
        /// </summary>
        public void Produce()
        {
            ObjectModel.SetActive(true);
        }
    }
}


