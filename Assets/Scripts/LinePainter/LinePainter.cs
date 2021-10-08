using UnityEngine;

/// <summary>
/// Малювальник ліній.
/// </summary>
public class LinePainter : MonoBehaviour
{
    /// <summary>
    /// Поточний LineRenderer
    /// </summary>
    private LineRenderer _currentLineRenderer;
    /// <summary>
    /// Стартова позиція поточної лінії по Z
    /// </summary>
    private float _currentLineStartPositionZ;
    /// <summary>
    /// Трансформ рівня (для руху)
    /// </summary>
    [SerializeField] private Transform Level;
    /// <summary>
    /// Запас чорнил
    /// </summary>
    [SerializeField] private float _inkCapacity;
    /// <summary>
    /// Відступ лінії по Y
    /// </summary>
    [SerializeField] private float _lineYOffset;
    /// <summary>
    /// Префаб лінії
    /// </summary>
    [SerializeField] private GameObject _linePrefab;
    /// <summary>
    /// Об'єкт у який додаються "погані" лінії 
    /// </summary>
    [SerializeField] private GameObject badLines;

    [SerializeField] private InkController _inkController;


    
    private void FixedUpdate()
    {
        if (_currentLineRenderer != null)
        {
            DrawLine();
        }
    }

    /// <summary>
    /// Відмальовує лінію за кінчиком пензлика
    /// </summary>
    public void DrawLine()
    {
        if (_inkController.CapacityGreaterThanZero)
        {
            Vector3 currentEndPosition = _currentLineRenderer.GetPosition(1);
            _currentLineRenderer.SetPosition(1, new Vector3(currentEndPosition.x, currentEndPosition.y, -(Level.position.z  - _currentLineStartPositionZ)));
            _inkController.DecreaseCapacityPerSec();
        }
       
    }

    /// <summary>
    /// Створює лінію
    /// </summary>
    /// <param name="lineContainer">Об'єкт у якому зберігатиметься лінія</param>
    private void CreateLine(LineContainer lineContainer)
    {
        var brushTipPosition = transform.position;


        var obj = Instantiate(_linePrefab,
            new Vector3(brushTipPosition.x, lineContainer.transform.position.y + _lineYOffset, brushTipPosition.z),
            Quaternion.identity);
        _currentLineRenderer = obj.GetComponent<LineRenderer>();
        _currentLineStartPositionZ = Level.position.z;

        lineContainer.SetLineToContainer(_currentLineRenderer.transform);
    }

    /// <summary>
    /// Визначає чи створювати лінію, і якщо так, то в якому об'єкті
    /// </summary>
    /// <param name="other">Колайдер у який входить пензлик</param>
    private void OnTriggerEnter(Collider other)
    {
        if (_inkController.CapacityGreaterThanZero)
        {
            LineContainer lineContainer = other.GetComponent<LineContainer>();
            if (lineContainer!= null)
            {
                CreateLine(lineContainer);
                Debug.Log("I`m here! I`m creating a line!");
            }
            
        }
    }

    /// <summary>
    /// Визначає чи потрібно переривати лінію, якщо лінія не належить контейнеру на якому знаходиться колайдер з яким взяємодіє пензлик, то непотрібно
    /// </summary>
    /// <param name="other">Колайдер, з якого виходить пензлик</param>
    private void OnTriggerExit(Collider other)
    {
        if (_currentLineRenderer == null)
        {
            return;
        }
        else
        {
            LineContainer lineContainer = other.GetComponent<LineContainer>();
            if (lineContainer != null)
            {
                if (!_currentLineRenderer.transform.parent.name.Equals(lineContainer.Container.name))
                {
                    return;
                }
            }
        }
        
        CutLine();
        Debug.Log("Paper Exit");
    }


    /// <summary>
    /// Зупиняє процес промальовки лінії
    /// </summary>
    public void CutLine()
    {
        if(_currentLineRenderer != null)
        {
            _currentLineRenderer = null;
            _currentLineStartPositionZ = 0;
        }
        
    }
}
