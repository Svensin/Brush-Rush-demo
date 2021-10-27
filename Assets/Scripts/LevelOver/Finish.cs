using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Stops level when brush reaches finish zone
/// </summary>
public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ScriptReferences.Instance.levelController.OnLevelFinish();
    }


}
