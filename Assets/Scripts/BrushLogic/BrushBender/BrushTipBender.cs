using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Клас що згинає кінчик пензля при взаємодії йлого з якоюсь поверхнею
/// </summary> 
public class BrushTipBender : MonoBehaviour
{
    /// <summary>
    /// Посиланна на аніматор меша пензлика
    /// </summary>     
    [SerializeField]private Animator tipAniamtor;

    /// <summary>
    /// Змінна що вказує на те чи торкається пензлик поверхні, потрібна для точності визначення взаємодії кінчика при зміні поверхні з якою взяємодіє пензлик
    /// </summary>
    private bool isTriggered = false;


    /// <summary>
    /// При взаємодії кінчик пензлика згинається 
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        tipAniamtor.SetBool("Bend", true);
        isTriggered = true;
    }

    /// <summary>
    /// Потрібен для додаткової перевірки при моментальній зміні поверхні з яким взаємодіє пензлик 
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
    /// При припененні взаємодії кінчик пензля розгинається
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        tipAniamtor.SetBool("Bend", false);
        isTriggered = false;
    }

}
