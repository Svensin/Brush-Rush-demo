using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerBarNotifier : MonoBehaviour
{
    
    /// <summary>
    /// Actvates anger bar script and UI element, also stops anger decreasing
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {

        ScriptReferences.Instance.angerBar.IsAngerDecreasing = false;
        ScriptReferences.Instance.angerBar.ActivateAngerProgressBar();
    }

    /// <summary>
    /// Increases anger bar value while brush collides with table
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        ScriptReferences.Instance.angerBar.IncreaseAnger();
    }

    /// <summary>
    /// Starts to decrease anger bar value, until brush doesn`t collide with table again
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        ScriptReferences.Instance.angerBar.IsAngerDecreasing = true;
    }
}
