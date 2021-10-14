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
    /// Поточний <see cref="GameState"/> гри.
    /// </summary>
    public GameState CurrentGameState { get; private set; }

    public static BlocksSpawner.Section CurrentSection { get; private set; }
    
    public static int CurrentLevel
    {
        get => _currentLevel;
        set => SetCurrentSection(value);
    }
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
    /// <see cref="GameObject"/> головного меню <see cref="MenuUiHandler"/>.
    /// </summary>
    [SerializeField] private Canvas Menu;
    /// <summary>
    /// <see cref="GameObject"/> UI на рівні.
    /// </summary>
    [SerializeField] private GameObject levelUi;
    /// <summary>
    /// <see cref="GameObject"/> UI на рівні.
    /// </summary>
    public GameObject LevelUI => levelUi;
    
    /// <summary>
    /// Канвас поразки
    /// </summary>
    [SerializeField] GameObject lossCanvas;


    public CompleteLevelMenu completeLevelMenu;

    /// <summary>
    /// Посилання на слайдер результатів рівня
    /// </summary>
    [SerializeField] private VictorySlider victorySlider;

    private bool _firstPhaseCoroutineEnded = false;

    /// <summary>
    /// Стан <see cref="LevelLogic.LevelMovement"/>.
    /// </summary>
    private enum EndLevelState
    {
        /// <summary>
        /// <see cref="LevelLogic.LevelMovement"/> виграно.
        /// </summary>
        Victory,
        /// <summary>
        /// <see cref="LevelLogic.LevelMovement"/> програно.
        /// </summary>
        Loss
    }

    /// <summary>
    /// Поточний стан рівня.
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
    /// Викликається при програші
    /// </summary>
    public void OnLoss()
    {
        SaveLoadSystem.Instance.SaveData();
        StopLevel();

        lossCanvas.SetActive(true);
    }


    /// <summary>
    /// Зупиняє <see cref="LevelLogic.LevelMovement"/> та вимикає усі важливі скрипти із <see cref="ScriptReferences"/>.
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
    /// При завершенні рівня зупиняє його та запускає, відповідно до <see cref="Results"/>, першу фазу <see cref="ShowFirstPhase"/> і <see cref="DefineLevelState"/>.
    /// </summary>
    public void OnLevelFinish()
    {
        StopLevel();

        Results results = ScriptReferences.Instance.levelFinalResultsCalculator.CalculateLevelResults();

        StartCoroutine(ShowFirstPhase(results.PaperPaintedRatio));

        StartCoroutine(DefineLevelState(results));

    }

    /// <summary>
    /// Визначає поточний стан закінчення <see cref="LevelLogic.LevelMovement"/>.
    /// </summary>
    /// <param name="results">отримані <see cref="Results"/></param>
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
    /// Перша фаза - показується прогрес та к-ть зароблених клаптиків.
    /// </summary>
    /// <param name="paperPaintedRatio">відсоток <see cref="Results.PaperPaintedRatio"/></param>
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
    /// Починає <see cref="LevelLogic.LevelMovement"/> та вмикає усі важливі скрипти з <see cref="ScriptReferences"/> для початку рівня.
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

    public void EnableControl()
    {
        ScriptReferences.Instance.brushControllerScript.enabled = true;
    }
    
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
