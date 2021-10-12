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
    /// Max value of anger bar
    /// </summary>
    [SerializeField] private float maxAngerValue;
    /// <summary>
    /// Current value of anger bar
    /// </summary>
    private float _currentAngerValue;

    /// <summary>
    /// How much the anger bar value decrease per second
    /// </summary>
    [SerializeField] private float decreaseDeltaAnger;
    /// <summary>
    /// How much the anger bar value increase per second
    /// </summary>
    [SerializeField] private float increaseDeltaAnger;

    /// <summary>
    /// Reference to anger bar Game Object
    /// </summary>
    [SerializeField] private GameObject AngerProgressBar;

    [SerializeField] private Image FillAngerBar;
    /// <summary>
    /// Defines if anger bar should be decreasing
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
    /// Reference to line painter script
    /// </summary>
    [SerializeField] private LinePainter _linePainter;
    

    private void FixedUpdate()
    {
        if (_currentAngerValue > 0 && angerDecreasing)
        {
            DecreaseAnger();
        }
    }

    /// <summary>
    /// Decreasing anger bar value. Should be called once per frame
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
    /// Increasing anger bar value. Should be called once per frame
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

    /// <summary>
    /// Pauses script`s work and deactivates UI element
    /// </summary>
    public void DeactivateAngerProgressBar()
    {
        AngerProgressBar.gameObject.SetActive(false);
    }

    public void ActivateAngerProgressBar()
    {
        AngerProgressBar.gameObject.SetActive(true);
    }

    /// <summary>
    /// Unpauses script`s work and activates UI element
    /// </summary>
    private void Awake()
    {
        _currentAngerValue = 0;
        FillAngerBar.fillAmount = _currentAngerValue;
    }
}
