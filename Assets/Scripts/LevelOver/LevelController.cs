using System;
using System.Collections;
using DefaultNamespace;
using LevelLogic;
using UiLogic;
using UnityEngine;
using Utilities;

public class LevelController : MonoBehaviour
{
    /// <summary>
    /// Current state of the game <see cref="GameState"/> гри.
    /// </summary>
    public GameState CurrentGameState { get; private set; }

    public static BlocksSpawner.Section CurrentSection { get; private set; }
    
    public static int CurrentLevel
    {
        get => _currentLevel;
        set => SetCurrentSection(value);
    }

    /// <summary>
    /// Sets what section has to be current
    /// </summary>
    /// <param name="value"></param>
    private static void SetCurrentSection(int value)
    {
        if (value > 5 || value < 0)
        {
            _currentLevel = 0;
            CurrentSection = BlocksSpawner.Section.A;
            return;
        }
        
        if (value < 2)
        {
            CurrentSection = BlocksSpawner.Section.A;
        }
        else if (value < 4)
        {
            CurrentSection = BlocksSpawner.Section.B;
        }
        else
        {
            CurrentSection = BlocksSpawner.Section.C;
        }

        _currentLevel = value;
    }
    
    private static int _currentLevel;
    
    private BlocksSpawner _menuBlocksSpawner;
    private BlocksSpawner _levelBlocksSpawner;
    private LevelMovement _level;
    private MenuCarrousel _menuCarrousel;
    
    /// <summary>
    /// <see cref="GameObject"/> main menu canvas <see cref="MenuUiHandler"/>.
    /// </summary>
    [SerializeField] private Canvas Menu;
    /// <summary>
    /// <see cref="GameObject"/> Level UI.
    /// </summary>
    [SerializeField] private GameObject levelUi;
    /// <summary>
    /// <see cref="GameObject"/> Level UI.
    /// </summary>
    public GameObject LevelUI => levelUi;
    
    /// <summary>
    /// Canvas of lost level
    /// </summary>
    [SerializeField] GameObject lossCanvas;


    public CompleteLevelMenu completeLevelMenu;

    /// <summary>
    /// reference to victory slider
    /// </summary>
    [SerializeField] private VictorySlider victorySlider;

    private bool _firstPhaseCoroutineEnded = false;

    /// <summary>
    /// Current state of level at the end <see cref="LevelLogic.LevelMovement"/>.
    /// </summary>
    private enum EndLevelState
    {
        /// <summary>
        /// <see cref="LevelLogic.LevelMovement"/> Level is won.
        /// </summary>
        Victory,
        /// <summary>
        /// <see cref="LevelLogic.LevelMovement"/> Level is lost.
        /// </summary>
        Loss
    }

    /// <summary>
    /// Current state at the end of the level.
    /// </summary>
    private EndLevelState _state;

    private void Awake()
    {
        _menuBlocksSpawner = ScriptReferences.Instance.menuBlockSpawner;
        _levelBlocksSpawner = ScriptReferences.Instance.levelBlockSpawner;
        _level = ScriptReferences.Instance.levelScript;
        _menuCarrousel = ScriptReferences.Instance.menuCarrousel;
    }

    /// <summary>
    /// Called then level is lost
    /// </summary>
    public void OnLoss()
    {
        SaveLoadSystem.Instance.SaveData();
        StopLevel();

        lossCanvas.SetActive(true);
    }


    /// <summary>
    /// Stops <see cref="LevelLogic.LevelMovement"/> and stop important scripts from <see cref="ScriptReferences"/>.
    /// </summary>
    private void StopLevel()
    {
        _level.StopLevelMovement();
        ScriptReferences.Instance.linePainterScript.CutLine();
        ScriptReferences.Instance.brushControllerScript.enabled = false;
        ScriptReferences.Instance.linePainterScript.enabled = false;
        ScriptReferences.Instance.inkControllerScript.enabled = false;

        var angerBarScripts = FindObjectsOfType<AngerBar>();

        foreach (AngerBar angerBarScript in angerBarScripts)
        {
            angerBarScript.enabled = false;
        }
    }

    /// <summary>
    /// Then level ends, stops level movement and accordigngly to <see cref="Results"/>, first phase <see cref="ShowFirstPhase"/> and <see cref="DefineLevelState"/>.
    /// </summary>
    public void OnLevelFinish()
    {
        StopLevel();

        Results results = ScriptReferences.Instance.levelFinalResultsCalculator.CalculateLevelResults();

        StartCoroutine(ShowFirstPhase(results.PaperPaintedRatio));

        StartCoroutine(DefineLevelState(results));

    }

    /// <summary>
    /// Defines current state of level ending<see cref="LevelLogic.LevelMovement"/>.
    /// </summary>
    /// <param name="results">received <see cref="Results"/></param>
    /// <returns></returns>
    private IEnumerator DefineLevelState(Results results)
    {
        yield return new WaitUntil(() => _firstPhaseCoroutineEnded);

        if (_state == EndLevelState.Victory)
        {
            completeLevelMenu.OpenCompleteLevelMenu(results);
        }
        else if (_state == EndLevelState.Loss)
        {
            lossCanvas.SetActive(true);
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// First phase shows progress and quantity of earned paintings pieces
    /// </summary>
    /// <param name="paperPaintedRatio">percentage of <see cref="Results.PaperPaintedRatio"/></param>
    /// <returns></returns>
    private IEnumerator ShowFirstPhase(float paperPaintedRatio)
    {
        victorySlider.gameObject.SetActive(true);

        float t = 0f;
        var currentStage = victorySlider.CurrentStagePoint;
        currentStage.MoveNext();

        while (victorySlider.SliderBar.fillAmount <= paperPaintedRatio - 0.01f)
        {
            t += Time.deltaTime;

            victorySlider.SliderBar.fillAmount =
                Mathf.Lerp(0, paperPaintedRatio, t);

            if (currentStage.Current != null)
            {
                if (victorySlider.SliderBar.fillAmount >= currentStage.Current.StageValue
                    && currentStage.Current.State == StageState.Cross)
                {
                    currentStage.Current.ChangeState(StageState.Tick);
                    currentStage.MoveNext();
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        if (paperPaintedRatio > victorySlider.FirstStagePoint.StageValue)
        {
            completeLevelMenu.FirstPhaseFinished(this, true);
            _state = EndLevelState.Victory;
        }
        else
        {
            lossCanvas.SetActive(true);
            _state = EndLevelState.Loss;
        }

        victorySlider.gameObject.SetActive(false);
        _firstPhaseCoroutineEnded = true;
    }

    /// <summary>
    /// Activates <see cref="LevelLogic.LevelMovement"/> and important scripts from <see cref="ScriptReferences"/> для початку рівня.
    /// </summary>
    private void StartLevel()
    {
        CurrentGameState = GameState.PLaying;
        
        _level.enabled = true;
        _level.StartLevelMovement();
        //ScriptReferences.Instance.brushControllerScript.enabled = true;
        ScriptReferences.Instance.linePainterScript.enabled = true;
        ScriptReferences.Instance.inkControllerScript.enabled = true;
        Menu.enabled = false;
        levelUi.SetActive(true);


        var angerBarScripts = FindObjectsOfType<AngerBar>();

        foreach (AngerBar angerBarScript in angerBarScripts)
        {
            angerBarScript.enabled = true;
        }
    }

    /// <summary>
    /// Enables control for player
    /// </summary>
    public void EnableControl()
    {
        ScriptReferences.Instance.brushControllerScript.enabled = true;
    }
    
    /// <summary>
    /// Initializes level then it starts
    /// </summary>
    public void InitializeLevel()
    {
        _menuCarrousel.Disable();

        BlockData firstBlockData = _menuCarrousel.CurrentBlock;
        BlockData secondBlockData = firstBlockData.NextBlock;

        _menuCarrousel.RemoveUnreachablePapers(firstBlockData);
        
        ((MenuBlocksSpawner)_menuBlocksSpawner).ResetLineContainers(firstBlockData);
        ((MenuBlocksSpawner)_menuBlocksSpawner).ResetLineContainers(secondBlockData);
        
        firstBlockData.SetParentTo(_level.transform);
        secondBlockData.SetParentTo(_level.transform);
        
        _level.BlocksData.Clear();
        
        _level.BlocksData.Add(firstBlockData);
        _level.BlocksData.Add(secondBlockData);
        ((LevelBlocksSpawner)_levelBlocksSpawner).SpawnBlock(CurrentSection,secondBlockData.LastBlockPoint.position);
        
        StartLevel();
    }
}
