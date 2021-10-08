using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UiLogic;

/// <summary>
/// Клас для обрахунків виданої гравцю клаптиків
/// </summary>
public class LevelFinalResultsCalculator : MonoBehaviour
{
    /// <summary>
    /// Клас для обрахунків виданої гравцю клаптиків
    /// </summary>
    [SerializeField] Transform papersLinesContainer;
    /// <summary>
    /// Клас для обрахунків виданої гравцю клаптиків
    /// </summary>
    [SerializeField] Transform papers;
    /// <summary>
    /// Клас для обрахунків виданої гравцю клаптиків
    /// </summary>
    [SerializeField] VictorySlider victorySlider;

    /// <summary>
    /// Фінальна сумарна довжина ліній на папері при кінці рівня
    /// </summary>
    float linesLength = 0f;
    /// <summary>
    /// Сумарна довжина паперу на рівні
    /// </summary>
    float papersLength = 0f;

    /// <summary>
    /// Вирахування довжин ліній та паперу
    /// </summary>
    private void SetLinesAndPaperLength()
    {
        var paperLines = papersLinesContainer.GetComponentsInChildren<LineRenderer>();
        foreach (LineRenderer line in paperLines)
        {
            float lineLength = line.GetPosition(1).z - line.GetPosition(0).z;

            linesLength += lineLength;
        }
        foreach (BlockData blockData in ScriptReferences.Instance.levelScript.BlocksData)
        {
            foreach (Transform paper in blockData.BlockDataPapers)
            {
                float paperLength = paper.GetChild(0).localScale.z;

                papersLength += paperLength;
            }
        }
        
    }

    /// <summary>
    /// Вирахування відношення довжини ліній до довжини паперу
    /// </summary>
    private float CalculatePercentage()
    {
        float result = linesLength / papersLength;
        return result;
    }

    /// <summary>
    /// Вирахування кількості клаптиків що має бути видано в кінці рівня
    /// </summary>
    private int CalculateImage(float percentRatio)
    {
        int piecesNumber = 0;

        if (victorySlider.FirstStagePoint.StageValue > percentRatio)
        {
            return piecesNumber;
        }

        piecesNumber = victorySlider.MinPiecesQuantity;

        for (int i = 1; i < victorySlider.StagePoints.Count; i++)
        {
            if (victorySlider.StagePoints[i].StageValue <= percentRatio)
            {
                piecesNumber++;
            }
            else
            {
                break;
            }
        }

        return piecesNumber;
    }

    /// <summary>
    /// Вирахування загального результату рівня
    /// </summary>
    public Results CalculateLevelResults()
    {
        SetLinesAndPaperLength();


        float percentRatio = CalculatePercentage();

        int imagePiecesNumber = CalculateImage(percentRatio);
        
        return new Results() { ImagePiecesCount = imagePiecesNumber, PaperPaintedRatio = percentRatio };
    }
}
