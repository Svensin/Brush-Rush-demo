using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineContainer : MonoBehaviour
{
    /// <summary>
    /// Object which will be parent obeject for lines
    /// </summary>
    [SerializeField] private Transform container;

    /// <summary>
    /// public property which allows to work with container then needed
    /// </summary>
    public Transform Container { get => container; set => container = value; }


    /// <summary>
    /// puts line into container(parent object)
    /// </summary>
    /// <param name="line">Line which will be saved in container</param> 
    public void SetLineToContainer(Transform line)
    {
        line.parent = container;
    }
}
