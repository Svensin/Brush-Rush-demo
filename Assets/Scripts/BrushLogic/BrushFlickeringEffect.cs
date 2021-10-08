using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Відповідає за ефект мигання(зникання/появи) пензлика
/// </summary> 
public class BrushFlickeringEffect : MonoBehaviour
{
    /// <summary>
    /// Скільки повинен тривати ефект, в секундах
    /// </summary>
    [SerializeField] float duration;
    /// <summary>
    /// Тривалість появи/зникання пензля
    /// </summary>
    [SerializeField] float oneFlickDuration;
    /// <summary>
    /// Посилання на модель пензля
    /// </summary>
    [SerializeField] GameObject brushModel;
    /// <summary>
    /// Скільки лишилось пензлику блимати в секундах
    /// </summary>
    private float currenrEffectTimeLeft;

    private void Start()
    {
        currenrEffectTimeLeft = duration;
    }

    /// <summary>
    /// Початок ефекту блимання або продовження якщо ефекти в момент виклику метода активний
    /// </summary>
    public void StartEffect()
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
    /// Корутіна ефекту блимання
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
