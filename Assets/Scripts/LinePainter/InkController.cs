using System;
using UnityEngine;
using UnityEngine.UI;
using LevelLogic;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

/// <summary>
/// Контролює рівень чорнила
/// </summary>
public class InkController : MonoBehaviour
{
    /// <summary>
    /// Поточний запас чорнил
    /// </summary>
    [SerializeField] [Min(0)] private float _currentInkCapacity;
    /// <summary>
    /// Гетер для поточного рівня чорнила
    /// </summary>
    public float CurrentInkCapacity => _currentInkCapacity;
    /// <summary>
    /// Максимальний рівень чорнила
    /// </summary>
    [SerializeField] [Min(1)] private float _maxInkCapacity;
    /// <summary>
    /// Гетер для максимального рівня чорнила
    /// </summary>
    public float MaxInkCapacity => _maxInkCapacity;
    /// <summary>
    /// Витрата чорнил за секунду
    /// </summary>
    [SerializeField] [Min(1)] private float _inkUsagePerSec;

    private float _initialInkUsagePerSec;

    public float InkUsagePer
    {
        set
        {
            if(value >0)
            {
                _inkUsagePerSec = value;
            }
        }
        get
        {
            return _inkUsagePerSec;
        }
    }

    /// <summary>
    /// Шкала рівня чорнил
    /// </summary>
    [SerializeField] private Slider _inkProgressBar;
    /// <summary>
    /// Посилання на клас Level
    /// </summary>
    [SerializeField] Level level;

    /// <summary>
    /// Перевірка запасу
    /// </summary>
    public bool CapacityGreaterThanZero => _currentInkCapacity > 0;

    /// <summary>
    /// Зменшити запас
    /// </summary>
    /// <param name="value">значення, на яке зменшити</param>
    public void DecreaseCapacity(float value, bool isDeadly = false)
    {
        if (_currentInkCapacity > 0)
        {
            _currentInkCapacity -= value;
        }
        if (_currentInkCapacity <= 0)
        {
            if (!isDeadly)
            {
                _currentInkCapacity = 0.0f;
            }
            else
            {
                _currentInkCapacity = 0.0f;

                ScriptReferences.Instance.levelController.OnLoss();
            }
            
        }
        
        _inkProgressBar.value = _currentInkCapacity/_maxInkCapacity;
    }

    /// <summary>
    /// Зменшити запас
    /// </summary>
    public void DecreaseCapacityPerSec()
    {
        if (_currentInkCapacity > 0)
        {
            _currentInkCapacity -=  _inkUsagePerSec * Time.fixedDeltaTime;
        }
        if (_currentInkCapacity <= 0)
        {
            _currentInkCapacity = 0.0f;
        }

        _inkProgressBar.value = _currentInkCapacity/_maxInkCapacity;
    }
    
    /// <summary>
    /// Поповнити запас
    /// </summary>
    /// <param name="value">значення, на яке поповнити</param>
    public void IncreaseCapacity(float value)
    {
        if (_currentInkCapacity < _maxInkCapacity)
        {
            _currentInkCapacity += value;
        }
        if (_currentInkCapacity >= _maxInkCapacity)
        {
            _currentInkCapacity = _maxInkCapacity;
        }
        
        _inkProgressBar.value = _currentInkCapacity/_maxInkCapacity;
    }
    
    /// <summary>
    /// Поповнити запас
    /// </summary>
    public void IncreaseCapacityPerSec()
    {
        if (_currentInkCapacity < _maxInkCapacity)
        {
            _currentInkCapacity += _inkUsagePerSec * Time.fixedDeltaTime;
        }
        if (_currentInkCapacity >= _maxInkCapacity)
        {
            _currentInkCapacity = _maxInkCapacity;
        }
        
        _inkProgressBar.value = _currentInkCapacity/_maxInkCapacity;
    }

    /// <summary>
    /// Вставновити запас чорнила
    /// </summary>
    /// <param name="value">значення у відсотках (від 0 до 1)</param>
    public void SetCapacityPercentage(float value, bool isDeadly = false)
    {
        

        if (value >= 1)
        {
            _currentInkCapacity = _maxInkCapacity;
        }
        else if(value >= 0)
        {
            _currentInkCapacity = _maxInkCapacity * value;
        }
        else
        {
            _currentInkCapacity = 0;

            ScriptReferences.Instance.levelController.OnLoss();
        }

        _inkProgressBar.value = _currentInkCapacity / _maxInkCapacity;
    }

    /// <summary>
    /// Помножити значення inkUsagePerSec, можна викликати тільки з пастки
    /// </summary>
    /// <param name="trap">посилання на пастку яка змінить inkUsagePerSec</param>
    /// <param name="inkUsagePerSecMupltiplier">значення на яке зміниться </param>
    public void MultipyInkUsagePerSec(object trap, float inkUsagePerSecMupltiplier)
    {
        if(trap is Interactables.IInteractable)
        {
            _inkUsagePerSec *= inkUsagePerSecMupltiplier;
        }
    }

    /// <summary>
    /// Повертає _inkUsagePerSec до початкового значення 
    /// </summary>
    /// /// <param name="trap">посилання на пастку яка змінить inkUsagePerSec</param>
    public void SetInitialInkUsagePerSec(object trap)
    {
        if (trap is Interactables.IInteractable)
        {
            _inkUsagePerSec = _initialInkUsagePerSec;
        }
    }

    private void Awake()
    {
        _currentInkCapacity = _maxInkCapacity;
        _inkUsagePerSec *= level.Velocity;
        _initialInkUsagePerSec = _inkUsagePerSec;
    }
}
