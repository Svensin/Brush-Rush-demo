using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngerBarNotifier : MonoBehaviour
{
    // Start is called before the first frame update

    
    private void OnTriggerEnter(Collider other)
    {

        ScriptReferences.Instance.angerBar.IsAngerDecreasing = false;
        ScriptReferences.Instance.angerBar.ActivateAngerProgressBar();
    }

    private void OnTriggerStay(Collider other)
    {
        //збільшуємо "злість"
        ScriptReferences.Instance.angerBar.IncreaseAnger();
    }

    private void OnTriggerExit(Collider other)
    {
        ScriptReferences.Instance.angerBar.IsAngerDecreasing = true;
    }
}
