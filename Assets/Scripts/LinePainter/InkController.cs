using System;
using UnityEngine;
using UnityEngine.UI;
using LevelLogic;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

/// <summary>
/// Controls ink capacity
/// </summary>
public class InkController : MonoBehaviour
{
    /// <summary>
    /// Current ink capacity value
    /// </summary>
    [SerializeField] [Min(0)] private float _currentInkCapacity;
    /// <summary>
    /// Current ink capacity value
    /// </summary>
    public float CurrentInkCapacity => _currentInkCapacity;
    /// <summary>
    /// Maximal ink capacity value
    /// </summary>
    [SerializeField] [Min(1)] private float _maxInkCapacity;
    /// <summary>
    /// Maximal ink capacity value
    /// </summary>
    public float MaxInkCapacity => _maxInkCapacity;
    /// <summary>
    /// How much ink capacity is used for 1 second of painting
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
    /// Ink capacity bar
    /// </summary>
    [SerializeField] private Slider _inkProgressBar;
    /// <summary>
    /// Reference to level movement script
    /// </summary>
    [SerializeField] LevelMovement level;

    /// <summary>
    /// Is there still some ink left
    /// </summary>
    public bool CapacityGreaterThanZero => _currentInkCapacity > 0;

    /// <summary>
    /// Decreases ink capacity
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
    /// Decreases ink capacity. Should be called once per frame
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
    /// Increases ink capacity
    /// </summary>
    /// <param name="value">how many you to substract from ink capacity</param>
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
    /// Increase ink capacity. Should be called once per frame
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
    /// Sets ink capacity to a certain percentage
    /// </summary>
    /// <param name="value">value from 0 to 1</param>
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
    /// Multiplies inkUsagePerSec. Can be called only from a trap class
    /// </summary>
    /// <param name="trap">reference to a trap script from there method is called</param>
    /// <param name="inkUsagePerSecMupltiplier">multiply value</param>
    public void MultipyInkUsagePerSec(object trap, float inkUsagePerSecMupltiplier)
    {
        if(trap is Interactables.IInteractable)
        {
            _inkUsagePerSec *= inkUsagePerSecMupltiplier;
        }
    }

    /// <summary>
    /// Sets _inkUsagePerSec to initial value. Can be called only from a trap class  
    /// </summary>
    /// /// <param name="trap">reference to a trap script from there method is called</param>
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
