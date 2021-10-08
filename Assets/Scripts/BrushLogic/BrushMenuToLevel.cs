using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BrushMenuToLevel : MonoBehaviour
{
    [SerializeField] private float brushLevelPositionLerp = 0.5f;
    [SerializeField] [Min(0.1f)] private float gettingBackSpeed;

    [SerializeField] private Transform brush;
    [SerializeField] private Transform minPositionTransform;
    [SerializeField] private Transform maxPositionTransform;
    [SerializeField] private Animator brushLogicAniamtor;
    // Start is called before the first frame update
    

    public void GetBackToLevelPosition()
    {
        StartCoroutine(GetBackToLevelPositionCoroutine());
    }

    private IEnumerator GetBackToLevelPositionCoroutine()
    {
        float brushPosRotationLerp = 0f;

        Vector3 menuBrushPos = brush.transform.position;
        Vector3 levelBrushPos = Vector3.Lerp(minPositionTransform.position, maxPositionTransform.position
            , brushLevelPositionLerp);

        Quaternion menuBrushRotation = brush.transform.rotation;

        brushLogicAniamtor.enabled = false;

        while (brushPosRotationLerp < 1)
        {
            brushPosRotationLerp += gettingBackSpeed * Time.deltaTime;

            brush.transform.position = Vector3.Lerp(menuBrushPos, levelBrushPos, brushPosRotationLerp);
            brush.transform.rotation = Quaternion.Lerp(menuBrushRotation, Quaternion.Euler(0, 0, 0), 
                brushPosRotationLerp);

            yield return new WaitForEndOfFrame();
        }

        brush.transform.position = Vector3.Lerp(menuBrushPos, levelBrushPos, 1f);
        brush.transform.rotation = Quaternion.Lerp(menuBrushRotation, Quaternion.Euler(0, 0, 0),1f);

        ScriptReferences.Instance.levelController.EnableControl();
    }
}
