using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDetector : MonoBehaviour
{
    [SerializeField] BrushTipBender brushTipBender;

    private void OnTriggerEnter(Collider other)
    {
        //brushTipBender.SetAsBendable();
    }

    private void OnTriggerExit(Collider other)
    {
        //brushTipBender.SetAsNotBendable();
    }
}
