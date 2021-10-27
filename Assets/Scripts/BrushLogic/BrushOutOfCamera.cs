using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which deactivates brush model then brush is out of camera`s sight
/// </summary>
public class BrushOutOfCamera : MonoBehaviour
{
    /// <summary>
    /// Reference to brush rigidbody
    /// </summary>
    [SerializeField] private Rigidbody _rigidbody;

    /// <summary>
    /// deactivates brush rigidbody and model
    /// </summary>
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        _rigidbody.isKinematic = true;
    }

}
