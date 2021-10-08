using System;
using UnityEngine;

/// <summary>
/// Детектор паперу.
/// Спрацьовує на колайдер початку і кінця паперу.
/// Вимикає колайдер столу, якщо пензлик знаходиться над папером.
/// </summary>
public class ObjectDetector : MonoBehaviour
{
    /// <summary>
    /// Колайдер столу
    /// </summary>
    [SerializeField] BoxCollider tableCollider;

    /// <summary>
    /// Зміщення колайдера столу для того щоб стіл перестав колайдитись
    /// </summary>
    [Min(0)] [SerializeField] float tableColliderOffset;
    [Min(0)] [SerializeField] float brushMinPointOffset;
    [SerializeField] private float maxTableYColliderPosition;
    [SerializeField] Transform minPoint;
    [SerializeField] Transform brush;

    // TODO: відрефакторити методи що зміщують точку мінімуму

    /// <summary>
    /// Визначає у який колайдер входить детектор
    /// </summary>
    /// <param name="other">Колайдер у який входить</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Producable"))
        {
            foreach(BlockData blockData in ScriptReferences.Instance.levelScript.BlocksData)
            {
                Vector3 currentTablePosition = blockData.TableCollider.center;
               if (Math.Abs(currentTablePosition.y - maxTableYColliderPosition) < 0.02f)
                {
                    blockData.TableCollider.center = new Vector3(currentTablePosition.x, currentTablePosition.y - tableColliderOffset, currentTablePosition.z);
                }
              
            }
            
            minPoint.transform.position = new Vector3(minPoint.transform.position.x, minPoint.transform.position.y + brushMinPointOffset,
                minPoint.transform.position.z);

            if (Mathf.Abs(minPoint.position.y - brush.position.y) < brushMinPointOffset + 0.3f)
            {
                ScriptReferences.Instance.brushControllerScript.MoveBrushToPosition(0);
            }

            
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Absorbable"))
        {
            foreach (BlockData blockData in ScriptReferences.Instance.levelScript.BlocksData)
            {
                Vector3 currentTablePosition = blockData.TableCollider.center;
                if (Math.Abs(currentTablePosition.y - (-tableColliderOffset)) < 0.02f)
                {
                    blockData.TableCollider.center = new Vector3(currentTablePosition.x, currentTablePosition.y + tableColliderOffset, currentTablePosition.z);
                }
            }

            minPoint.transform.position = new Vector3(minPoint.transform.position.x, minPoint.transform.position.y - brushMinPointOffset,
                minPoint.transform.position.z);

            if (Mathf.Abs(brush.position.y - minPoint.position.y) < brushMinPointOffset+0.3f)
            {
                ScriptReferences.Instance.brushControllerScript.MoveBrushToPosition(0);
            }
        }
    }

}
