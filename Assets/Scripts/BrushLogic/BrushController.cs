using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контролює механіку переміщення пензлика
/// </summary>
public class BrushController : MonoBehaviour
{
    /// <summary> 
    /// Referenece to brush`s transform 
    /// </summary>
    [SerializeField] Transform brushTransform;
    /// <summary> 
    /// Minimal y value which brush`s transform can reach
    /// </summary>
    [SerializeField] Transform minPoint;
    /// <summary> 
    /// Maximal y value which brush`s transform can reach
    /// </summary>
    [SerializeField] Transform maxPoint;
    /// <summary> 
    /// Brush`s control sensitivity
    /// </summary>
    [SerializeField] float sensitivity;

    /// <summary> 
    /// Referenece to brush`s rigidbody
    /// </summary>
    private Rigidbody _brushRigidbody;
    /// <summary> 
    /// Height of device display in pixels 
    /// </summary>
    private float _screenHeight;
    /// <summary> 
    /// Lowest point on display in pixels player can slide to move the brush down
    /// </summary>
    private Vector2 _minTouchPosition;
    /// <summary> 
    /// Highest point on display in pixels player can slide to move the brush down 
    /// </summary>
    private Vector2 _maxTouchPosition;

    void Start()
    {
        _screenHeight = Screen.height;
        _brushRigidbody = brushTransform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {

            Touch firstTouch = Input.touches[0];
            if (firstTouch.phase.Equals(TouchPhase.Began)) {
                float brushInterpolation = CalculatePositionInterpolation(minPoint.position.y, maxPoint.position.y, brushTransform.position.y);

                DefineEdgeTouchPositions(firstTouch.position, brushInterpolation);
            }

            if(firstTouch.phase.Equals(TouchPhase.Moved) && firstTouch.position.y > _minTouchPosition.y && firstTouch.position.y < _maxTouchPosition.y) {
                MoveBrushToPosition(firstTouch.position);
            }

            
        }
    }

    /// <summary>
    /// Inverse interpolation of brush position beetwen max and min points
    /// </summary>
    /// <param name="startPointY">Min point y value</param>
    /// <param name="endPointY">Max point y value</param>
    /// <param name="interpolationPointY">Brush`s position y value</param>
    /// <returns>Коєфіцієнт інтерполяції</returns>
    public float CalculatePositionInterpolation(float startPointY, float endPointY, float interpolationPointY)
    {

        float distanceToInterpolationPoint = Mathf.Abs(interpolationPointY - startPointY);
        float distanceToMaxPoint = Mathf.Abs(endPointY - startPointY);

        float interpolation = distanceToInterpolationPoint / distanceToMaxPoint;

        return interpolation;
    }

    /// <summary>
    /// Defines min and max points on display which player can slide to
    /// </summary>
    /// <param name="touchPosition">Display postion of point where player has touched the screen to start brush movement</param>
    /// <param name="interpolation">interpalation value. Needed to calculate display min and max points</param>
    public void DefineEdgeTouchPositions(Vector2 touchPosition, float interpolation)
    {
        float touchSegmentLength = _screenHeight / sensitivity;

        _minTouchPosition = new Vector2(touchPosition.x, touchPosition.y - touchSegmentLength * interpolation);

        _maxTouchPosition = new Vector2(touchPosition.x, touchPosition.y + touchSegmentLength * (1-interpolation));
    }

    /// <summary>
    /// Moves brush to position where player touches screen
    /// </summary>
    /// <param name="touchPosition">Postion of player`s touch</param>
    /// <returns>New brush`s positon y value</returns>
    public float MoveBrushToPosition(Vector2 touchPosition)
    {
        float touchYPosition = touchPosition.y;
        float minTouchPointY = _minTouchPosition.y;
        float maxTouchPointY = _maxTouchPosition.y;

        float touchInterpolation = CalculatePositionInterpolation(minTouchPointY, maxTouchPointY, touchYPosition);

        Vector3 moveToPositon = Vector3.Lerp(minPoint.position, maxPoint.position, touchInterpolation);

        _brushRigidbody.MovePosition(moveToPositon);

        return moveToPositon.y;
    }

    /// <summary>
    /// Moves to brush to wanted position between min and max points 
    /// </summary>
    /// <param name="interpolation">interpolation value</param>
    /// <returns>New brush`s positon y value</returns>
    public float MoveBrushToPosition(float interpolation)
    {
        float minTouchPointY = _minTouchPosition.y;
        float maxTouchPointY = _maxTouchPosition.y;


        Vector3 moveToPositon = Vector3.Lerp(minPoint.position, maxPoint.position, interpolation);

        _brushRigidbody.MovePosition(moveToPositon);

        return moveToPositon.y;
    }
}
