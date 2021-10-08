using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GalleryLogic;
using UnityEngine;
using Random = System.Random;
using Utilities;

/// <summary>
/// Меню, яке показується після проходження <see cref="LevelLogic.Level"/>.
/// </summary>
public class CompleteLevelMenu : MonoBehaviour
{
    /// <summary>
    /// <see cref="Utilities.SaveLoadData.Painting.pieces"/>, що закривають картину
    /// </summary>
    private List<GameObject> pieces;

    /// <summary>
    /// Чи закінчилась перша фаза
    /// </summary>
    private bool _firstPhaseFinished;

    /// <summary>
    /// Перевіряє чи перша фаза <see cref="LevelController"/> після проходження рівня закінчена.
    /// </summary>
    /// <param name="sender">ініціатор виклику</param>
    /// <param name="value">значення</param>
    public void FirstPhaseFinished(object sender, bool value)
    {
        if (sender is LevelController || sender is CompleteLevelMenu)
        {
            _firstPhaseFinished = value;
        }
        else
        {
            Debug.LogError($"This object cannot modify the {nameof(CompleteLevelMenu)} state!");
        }
    }

    /// <summary>
    /// Дублікат картини з галереї на VictoryCanvas
    /// </summary>
    private GameObject _duplicatePainting;
    
    /// <summary>
    /// Шаблон для картин
    /// </summary>
    [SerializeField] private RectTransform templateRectTransform;


    /// <summary>
    /// Посилання на меню перемоги рівня
    /// </summary>
    [SerializeField] private GameObject victoryCanvas;

    /// <summary>
    /// Відкрити меню виграшу
    /// </summary>
    /// <param name="levelResults">результати рівня</param>
    public void OpenCompleteLevelMenu(Results levelResults)
    {
        SetImagePieces();
        ProcessResults(levelResults);
    }

    
    /// <summary>
    /// Додає картину з <see cref="Gallery"/>.
    /// </summary>
    /// <returns>дублікат картини</returns>
    private GameObject AddPaintingFromGallery()
    {
        var duplicate = Instantiate(Gallery.Instance.CurrentPainting, victoryCanvas.transform);

        var rectTransform = duplicate.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
           templateRectTransform.rect.width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            templateRectTransform.rect.height);
        rectTransform.SetPositionAndRotation(templateRectTransform.transform.position,
            templateRectTransform.rotation);

        var duplicateGameObject = duplicate.gameObject;
        duplicateGameObject.SetActive(true);
       
        return duplicateGameObject;
    }

    /// <summary>
    /// Зібрати частини полотна
    /// </summary>
    private void SetImagePieces()
    {
        if (_duplicatePainting == null)
        {
            _duplicatePainting = AddPaintingFromGallery();
        }
        
        pieces = new List<GameObject>(_duplicatePainting.transform.childCount);
        foreach (Transform piece in _duplicatePainting.transform)
        {
            if (piece.gameObject.activeSelf)
            {
                pieces.Add(piece.gameObject);
            }
        }
    }
    
    /// <summary>
    /// Обробити отримані <see cref="Results"/>
    /// </summary>
    /// <param name="levelResults">результати рівня</param>
    private void ProcessResults(Results levelResults)
    {
        var imagePieces = ProcessImagePieces( levelResults.ImagePiecesCount);

        StartCoroutine(ShowSecondPhase(imagePieces));
    }
    
  

    /// <summary>
    /// Друга фаза - показується <see cref="Gallery.CurrentPainting"/>, зникають клаптики
    /// </summary>
    /// <param name="imagePiecesAnimators">аніматори ігрових-об'єктів-клаптиків</param>
    /// <returns></returns>
    private IEnumerator ShowSecondPhase(Animator[] imagePiecesAnimators)
    {
        yield return new WaitUntil(() => _firstPhaseFinished);
        
        foreach (var pieceAnimator in imagePiecesAnimators)
        {
            pieceAnimator.gameObject.SetActive(false);
        }
        
        //змінюємо картину в галереї
        Gallery.Instance.UpdateCurrentPainting(_duplicatePainting);
        
        LevelController.CurrentLevel++;

        //зберігаємо
        SaveLoadSystem.Instance.SaveData();
        
        foreach (var pieceAnimator in imagePiecesAnimators)
        {
            pieceAnimator.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(0.025f);
        
        victoryCanvas.SetActive(true);

        var animClip = imagePiecesAnimators[0].runtimeAnimatorController.animationClips
            .First(a => a.name == "PieceAnimation");
        
        imagePiecesAnimators[0].Play("PieceAnimation");
        yield return new WaitForSeconds(animClip.length);
        
        foreach (var pieceAnimator in imagePiecesAnimators)
        {
            pieceAnimator.enabled = false;
            pieceAnimator.gameObject.SetActive(false);
        }
       
    }

    /// <summary>
    /// Оброблюються частинки полотна.
    /// </summary>
    /// <param name="count">к-ть клаптиків</param>
    /// <returns>масив <see cref="GameObject"/> клаптиків</returns>
    private Animator[] ProcessImagePieces(int count)
    {
        if (count > pieces.Count)
        {
            count = pieces.Count;
        }
        
        var selectedPieces = new List<Animator>(count);

        for (int i = 0; i < count; i++)
        {
            var rand = new Random().Next(0, pieces.Count);

            var anim = pieces[rand].GetComponent<Animator>();
            anim.enabled = true;
            
            selectedPieces.Add(anim);
            pieces.RemoveAt(rand);
        }
        
        return selectedPieces.ToArray();
    }

}