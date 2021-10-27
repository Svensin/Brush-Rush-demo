using UnityEngine;

public class LinePainter : MonoBehaviour
{
    /// <summary>
    /// Current LineRenderer which Line Painter works with
    /// </summary>
    private LineRenderer _currentLineRenderer;
    /// <summary>
    /// Start position Z value
    /// </summary>
    private float _currentLineStartPositionZ;
    /// <summary>
    /// Transform of the level
    /// </summary>
    [SerializeField] private Transform Level;
    /// <summary>
    /// ink capacity
    /// </summary>
    [SerializeField] private float _inkCapacity;
    /// <summary>
    /// little line end offset on y value
    /// </summary>
    [SerializeField] private float _lineYOffset;
    /// <summary>
    /// Line prefab
    /// </summary>
    [SerializeField] private GameObject _linePrefab;
    /// <summary>
    /// Bad lines(painted on table, not on paper) container 
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
    /// Draws line which follows brush tip
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
    /// Creates new line
    /// </summary>
    /// <param name="lineContainer">Game Object which will be the parent of a new line</param>
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
    /// Defines if line should be created and assigns this line to correct container
    /// </summary>
    /// <param name="other">Collider which brush tip collided with</param>
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
    /// Defines if line should be interrupted
    /// </summary>
    /// <param name="other">Collider which brush tip collided with</param>
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
    /// Stops line painting
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
