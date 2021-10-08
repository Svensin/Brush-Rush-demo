using UnityEngine;
using LevelLogic;
using System;

namespace LevelLogic
{
    /// <summary>
    /// Відповідає за деактивацію об'єктів рівня
    /// </summary>
    public class Absorber : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IDisposable objectDispose = other.transform.parent.GetComponent<IDisposable>();

            objectDispose.Dispose();

        }
    }
}