using System;
using LevelLogic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Шкала "злості"
/// </summary>
public class AngerBar : MonoBehaviour
{
    /// <summary>
    /// Максимальний рівень "злості"
    /// </summary>
    [SerializeField] private float maxAngerValue;
    /// <summary>
    /// Поточний рівень "злості"
    /// </summary>
    private float _currentAngerValue;

    /// <summary>
    /// Зменшення рівня "злості" щосекунди, якщо нема колізії зі столом
    /// </summary>
    [SerializeField] private float decreaseDeltaAnger;
    /// <summary>
    /// Збільшення рівня "злості" щосекунди, якщо є колізія зі столом
    /// </summary>
    [SerializeField] private float increaseDeltaAnger;

    /// <summary>
    /// Шкала для "злості"
    /// </summary>
    [SerializeField] private GameObject AngerProgressBar;

    [SerializeField] private Image FillAngerBar;
    /// <summary>
    /// Чи зменшується значення шкали "злості"
    /// </summary>
    private bool angerDecreasing;

    public bool IsAngerDecreasing
    {
        set
        {
            angerDecreasing = value;
        }
    }
    
    /// <summary>
    /// Посилання на LinePainter
    /// </summary>
    [SerializeField] private LinePainter _linePainter;
    
    /// <summary>
    /// Колайдер столу
    /// </summary>
    //private Collider _tableCollider;

    ///// <summary>
    ///// Гетер колайдера стола для доступу іншим скриптам
    ///// </summary>
    //public Collider TableCollider => _tableCollider;


    private void FixedUpdate()
    {
        if (_currentAngerValue > 0 && angerDecreasing)
        {
            //зменшуємо "злість"
            DecreaseAnger();
        }
    }

    /// <summary>
    /// Зменшує рівень "злості"
    /// </summary>
    public void DecreaseAnger()
    {
        var currentDeltaTime = Time.deltaTime;
        if (_currentAngerValue - decreaseDeltaAnger * currentDeltaTime > 0)
        {
            _currentAngerValue -= decreaseDeltaAnger * currentDeltaTime;
        }
        else
        {
            AngerProgressBar.gameObject.SetActive(false);
            _currentAngerValue = 0;
        }
        FillAngerBar.fillAmount = _currentAngerValue/maxAngerValue;
    }

    /// <summary>
    /// Збільшує рівень "злості"
    /// </summary>
    public void IncreaseAnger()
    {
        var currentDeltaTime = Time.deltaTime;
        if (_currentAngerValue + increaseDeltaAnger * currentDeltaTime < maxAngerValue)
        {
            _currentAngerValue += increaseDeltaAnger * currentDeltaTime;
            FillAngerBar.fillAmount = _currentAngerValue/maxAngerValue;
        }
        else
        {
            ScriptReferences.Instance.levelController.OnLoss();
        }
        
    }

    public void DeactivateAngerProgressBar()
    {
        AngerProgressBar.gameObject.SetActive(false);
    }

    public void ActivateAngerProgressBar()
    {
        AngerProgressBar.gameObject.SetActive(true);
    }

    private void Awake()
    {
        _currentAngerValue = 0;
        FillAngerBar.fillAmount = _currentAngerValue;
    }
}
