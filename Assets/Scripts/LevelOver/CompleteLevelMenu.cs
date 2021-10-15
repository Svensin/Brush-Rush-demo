using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GalleryLogic;
using UnityEngine;
using Random = System.Random;
using Utilities;


public class CompleteLevelMenu : MonoBehaviour
{
    /// <summary>
    /// <see cref="Utilities.SaveLoadData.Painting.pieces"/>, which covers the painting
    /// </summary>
    private List<GameObject> pieces;

    /// <summary>
    /// Is first phase over
    /// </summary>
    private bool _firstPhaseFinished;

    /// <summary>
    /// Checks if first phase <see cref="LevelController"/> after level completion is over.
    /// </summary>
    /// <param name="sender">object who executes the method</param>
    /// <param name="value">if first phase is over</param>
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
    /// Duplicae of panting from gallery for VictoryCanvas
    /// </summary>
    private GameObject _duplicatePainting;
    
    /// <summary>
    /// Template for painting
    /// </summary>
    [SerializeField] private RectTransform templateRectTransform;


    /// <summary>
    /// Reference to victory menu
    /// </summary>
    [SerializeField] private GameObject victoryCanvas;

    /// <summary>
    /// Opens victory menu
    /// </summary>
    /// <param name="levelResults">results of the level</param>
    public void OpenCompleteLevelMenu(Results levelResults)
    {
        SetImagePieces();
        ProcessResults(levelResults);
    }

    
    /// <summary>
    /// Adds painting from <see cref="Gallery"/>.
    /// </summary>
    /// <returns>Duplicate of the painting</returns>
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
    /// Puts together pieces of the painting
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
    /// Process received <see cref="Results"/>
    /// </summary>
    /// <param name="levelResults"></param>
    private void ProcessResults(Results levelResults)
    {
        var imagePieces = ProcessImagePieces( levelResults.ImagePiecesCount);

        StartCoroutine(ShowSecondPhase(imagePieces));
    }
    
  

    /// <summary>
    /// On the second phase <see cref="Gallery.CurrentPainting"/> shows up, some pieces are vanishing
    /// </summary>
    /// <param name="imagePiecesAnimators">animators of pieces</param>
    /// <returns></returns>
    private IEnumerator ShowSecondPhase(Animator[] imagePiecesAnimators)
    {
        yield return new WaitUntil(() => _firstPhaseFinished);
        
        foreach (var pieceAnimator in imagePiecesAnimators)
        {
            pieceAnimator.gameObject.SetActive(false);
        }
        
        Gallery.Instance.UpdateCurrentPainting(_duplicatePainting);
        
        LevelController.CurrentLevel++;

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
    /// Processes all painting pieces
    /// </summary>
    /// <param name="count">pieces quantity</param>
    /// <returns>array <see cref="GameObject"/>of pieces</returns>
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