using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelLogic;
using UiLogic;
using Utilities.ScriptableObjects;

/// <summary>
/// ��������, ���� ������ ��������� �� �� ������ ������� ���
/// </summary>
public class ScriptReferences : SingletonComponent<ScriptReferences>
{
    [Header("BrushLogic scripts")] [Space]
    [SerializeField] public BrushController brushControllerScript;
    [SerializeField] public BrushFlickeringEffect brushFlickeringEffect;
    
    [Header("Ink&Lines scripts")] [Space]
    [SerializeField] public InkController inkControllerScript;
    [SerializeField] public LinePainter linePainterScript;
    
    [Header("LevelLogic scripts")][Space]
    [SerializeField] public LevelMovement levelScript;
    [SerializeField] public LevelFinalResultsCalculator levelFinalResultsCalculator;
    [SerializeField] public LevelController levelController;
    
    [Header("Progress Bars")][Space]
    [SerializeField] public AngerBar angerBar;

    [Header("Menu")][Space]
    [SerializeField] public MenuTableCycleCarrousel menuCarrousel;
    public MenuUiHandler menuUiHandler;
    
    [Header("Blocks spawners")][Space]
    [SerializeField] public BlocksSpawner levelBlockSpawner;
    [SerializeField] public BlocksSpawner menuBlockSpawner;

    [Header("Scriptable Objects")]
    public ScriptableObjectsLocator Locator;
}
