using UnityEngine;
using System;
using LevelLogic; 

namespace LevelLogic
{
    /// <summary>
    /// Відповідає за активацію об'єктів рівня
    /// </summary>
    public class Producer : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            IProducible objectToProduce = other.transform.parent.GetComponent<IProducible>();

            objectToProduce.Produce();

        }
    }
}