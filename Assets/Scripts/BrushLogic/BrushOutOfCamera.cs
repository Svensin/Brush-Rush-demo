using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Описує логіку пензлика, коли той поза камерою
/// </summary>
public class BrushOutOfCamera : MonoBehaviour
{
    /// <summary>
    /// Фізичне тіло пензлика
    /// </summary>
    [SerializeField] private Rigidbody _rigidbody;
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        _rigidbody.isKinematic = true;
    }

}
