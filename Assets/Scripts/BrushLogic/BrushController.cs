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
    /// Трансформ пензлика 
    /// </summary>
    [SerializeField] Transform brushTransform;
    /// <summary> 
    /// Нижній ліміт для пересування пензлика
    /// </summary>
    [SerializeField] Transform minPoint;
    /// <summary> 
    /// Верхній ліміт для пересування пензлика 
    /// </summary>
    [SerializeField] Transform maxPoint;
    /// <summary> 
    /// Чутливість слайдера 
    /// </summary>
    [SerializeField] float sensitivity;

    /// <summary> 
    /// Фізичне тіло пензлика 
    /// </summary>
    private Rigidbody _brushRigidbody;
    /// <summary> 
    /// Висота девайсу 
    /// </summary>
    private float _screenHeight;
    /// <summary> 
    /// Нижня точка слайдера 
    /// </summary>
    private Vector2 _minTouchPosition;
    /// <summary> 
    /// Верхня точка слайдера 
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
    /// Обраховує інтерпольоване значення між нижньою і верхньою точками слайдера
    /// </summary>
    /// <param name="startPointY">Нижня точка</param>
    /// <param name="endPointY">Верхня точка</param>
    /// <param name="interpolationPointY">Інтерпольована точка</param>
    /// <returns>Коєфіцієнт інтерполяції</returns>
    public float CalculatePositionInterpolation(float startPointY, float endPointY, float interpolationPointY)
    {

        float distanceToInterpolationPoint = Mathf.Abs(interpolationPointY - startPointY);
        float distanceToMaxPoint = Mathf.Abs(endPointY - startPointY);

        float interpolation = distanceToInterpolationPoint / distanceToMaxPoint;

        return interpolation;
    }

    /// <summary>
    /// Визначає крайні точки слайдера
    /// </summary>
    /// <param name="touchPosition">Точка дотику</param>
    /// <param name="interpolation">Значення коєфіцієнту інтерполяції</param>
    public void DefineEdgeTouchPositions(Vector2 touchPosition, float interpolation)
    {
        float touchSegmentLength = _screenHeight / sensitivity;

        _minTouchPosition = new Vector2(touchPosition.x, touchPosition.y - touchSegmentLength * interpolation);

        _maxTouchPosition = new Vector2(touchPosition.x, touchPosition.y + touchSegmentLength * (1-interpolation));
    }

    /// <summary>
    /// Пересуває пензлик на позицію дотику
    /// </summary>
    /// <param name="touchPosition">Точка дотику</param>
    /// <returns>Y-координата кінцевої позиції</returns>
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

    // TODO глянути на цей метод чи ок
    public float MoveBrushToPosition(float interpolation)
    {
        float minTouchPointY = _minTouchPosition.y;
        float maxTouchPointY = _maxTouchPosition.y;


        Vector3 moveToPositon = Vector3.Lerp(minPoint.position, maxPoint.position, interpolation);

        _brushRigidbody.MovePosition(moveToPositon);

        return moveToPositon.y;
    }
}
