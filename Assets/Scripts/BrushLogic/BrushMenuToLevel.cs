using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// Class dedicated to translate from menu state to level state
/// </summary>
public class BrushMenuToLevel : MonoBehaviour
{
    /// <summary>
    /// Default level interpolation value between lowest and highest brush level positions
    /// </summary>
    [SerializeField] private float brushLevelPositionLerp = 0.5f;
    /// <summary>
    /// how fast in seconds brush has to get to default level position
    /// </summary>
    [SerializeField] [Min(0.1f)] private float gettingBackSpeed;

    [SerializeField] private Transform brushTransform;
    /// <summary>
    /// Reference to lowest brush position
    /// </summary>
    [SerializeField] private Transform minPositionTransform;
    /// <summary>
    /// Reference to highest brush position
    /// </summary>
    [SerializeField] private Transform maxPositionTransform;
    /// <summary>
    /// Reference to brush model animator
    /// </summary>
    [SerializeField] private Animator brushLogicAniamtor;
    
    /// <summary>
    /// Starts coroutine which translate to default level position 
    /// </summary>
    public void GetBackToLevelPosition()
    {
        StartCoroutine(GetBackToLevelPositionCoroutine());
    }

    /// <summary>
    /// Stops brush rotation animation, translates brush to default level position, and activates brush control
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetBackToLevelPositionCoroutine()
    {
        float brushPosRotationLerp = 0f;

        Vector3 menuBrushPos = brushTransform.transform.position;
        Vector3 levelBrushPos = Vector3.Lerp(minPositionTransform.position, maxPositionTransform.position
            , brushLevelPositionLerp);

        Quaternion menuBrushRotation = brushTransform.transform.rotation;

        brushLogicAniamtor.enabled = false;

        while (brushPosRotationLerp < 1)
        {
            brushPosRotationLerp += gettingBackSpeed * Time.deltaTime;

            brushTransform.transform.position = Vector3.Lerp(menuBrushPos, levelBrushPos, brushPosRotationLerp);
            brushTransform.transform.rotation = Quaternion.Lerp(menuBrushRotation, Quaternion.Euler(0, 0, 0), 
                brushPosRotationLerp);

            yield return new WaitForEndOfFrame();
        }

        brushTransform.transform.position = Vector3.Lerp(menuBrushPos, levelBrushPos, 1f);
        brushTransform.transform.rotation = Quaternion.Lerp(menuBrushRotation, Quaternion.Euler(0, 0, 0),1f);

        ScriptReferences.Instance.levelController.EnableControl();
    }
}
