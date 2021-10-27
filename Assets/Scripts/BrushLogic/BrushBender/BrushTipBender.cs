using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Class, which bends brush's tip then brush paints
/// </summary> 
public class BrushTipBender : MonoBehaviour
{
    /// <summary>
    /// Reference to brush tip animator
    /// </summary>     
    [SerializeField]private Animator tipAniamtor;

    /// <summary>
    /// Defines if brush`s tip is triggered by something
    /// </summary>
    private bool isTriggered = false;


    /// <summary>
    /// Starts to bend brush`s tip
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        tipAniamtor.SetBool("Bend", true);
        isTriggered = true;
    }

    /// <summary>
    /// Additional check if brush`s tip is being triggered by something 
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            tipAniamtor.SetBool("Bend", true);
        }
        else
        {
            tipAniamtor.SetBool("Bend", true);
        }
    }

    /// <summary>
    /// Stops brush`s tip bending
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        tipAniamtor.SetBool("Bend", false);
        isTriggered = false;
    }

}
