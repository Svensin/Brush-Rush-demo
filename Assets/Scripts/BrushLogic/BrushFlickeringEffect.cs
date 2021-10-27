using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Відповідає за ефект мигання(зникання/появи) пензлика
/// </summary> 
public class BrushFlickeringEffect : MonoBehaviour
{
    /// <summary>
    /// How much time effect has to last in seconds
    /// </summary>
    [SerializeField] float duration;
    /// <summary>
    /// Duration of single appearance/disappearance of brush
    /// </summary>
    [SerializeField] float oneFlickDuration;
    /// <summary>
    /// Reference to brush model
    /// </summary>
    [SerializeField] GameObject brushModel;
    /// <summary>
    /// How many seconds left for effect to last
    /// </summary>
    private float currenrEffectTimeLeft;

    private void Start()
    {
        currenrEffectTimeLeft = duration;
    }

    /// <summary>
    /// Starts or resets effect duration
    /// </summary>
    public void StartOrContinueEffect()
    {
        if (currenrEffectTimeLeft == duration)
        {
            StartCoroutine(EffectCoroutine());
        }
        else
        {
            currenrEffectTimeLeft = duration;
        }
    }

    /// <summary>
    /// Starts to deactivate/activate brush model
    /// </summary>
    private IEnumerator EffectCoroutine()
    {
        while (currenrEffectTimeLeft > 0)
        {
            brushModel.SetActive(!brushModel.activeSelf);
            yield return new WaitForSeconds(oneFlickDuration);
            currenrEffectTimeLeft -= oneFlickDuration;
            Debug.Log(currenrEffectTimeLeft);
        }

        currenrEffectTimeLeft = duration;
        brushModel.SetActive(true);
    }
}
