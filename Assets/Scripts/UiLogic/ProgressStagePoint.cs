using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    /// <summary>
    /// Stage of rewarding <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
    /// </summary>
    public class ProgressStagePoint : MonoBehaviour
    {
        /// <summary>
        /// Number of the stage 
        /// </summary>
        [Range(0, 5)] [SerializeField] private int stageCount;
        
        /// <summary>
        /// Obecject-image of the check mark
        /// </summary>
        [SerializeField] private GameObject tickImage;
        /// <summary>
        /// Obecject-image of the uncheck mark
        /// </summary>
        [SerializeField] private GameObject crossImage;
        /// <summary>
        /// <see cref="Text.text"/> where quantity of the <see cref="Utilities.SaveLoadData.Painting.pieces"/> for stage displayed.
        /// </summary>
        [SerializeField] private Text stageText;

        /// <summary>
        /// Min-max values of <see cref="Results.PaperPaintedRatio"/> which are in stage.
        /// </summary>
        [Header("Stage values")]
        [SerializeField] private Vector2 minMaxStageValues;
        /// <summary>
        /// Currents value of <see cref="Results.PaperPaintedRatio"/> for stage.
        /// </summary>
        [Range(0,1f)][SerializeField] private float stageValue;
        
        public float StageValue => stageValue;

        /// <summary>
        /// Current stage state
        /// </summary>
        public StageState State { get; private set; }

        /// <summary>
        /// Changes <see cref="Text.text"/> for stage.
        /// </summary>
        /// <param name="piecesCount">к-ть <see cref="Utilities.SaveLoadData.Painting.pieces"/></param>
        public void ChangeStageText(int piecesCount = 0)
        {
            stageText.text = $"{piecesCount + stageCount}x";
        }

        /// <summary>
        /// Changes <see cref="StageState"/> for stage
        /// </summary>
        /// <param name="state"><see cref="StageState"/> of stage</param>
        public void ChangeState(StageState state)
        {
            if (state == StageState.Cross)
            {
                tickImage.SetActive(false);
                crossImage.SetActive(true);
            }
            else if (state == StageState.Tick)
            {
                crossImage.SetActive(false);
                tickImage.SetActive(true);
            }

            State = state;
        }
        
        private void OnValidate()
        {
            stageValue = Mathf.Clamp(stageValue, minMaxStageValues.x, minMaxStageValues.y);
        }
    }

    /// <summary>
    /// Stage of <see cref="ProgressStagePoint"/>.
    /// </summary>
    public enum StageState
    {
        /// <summary>
        /// <see cref="ProgressStagePoint"/> is unchecked.
        /// </summary>
        Cross,
        /// <summary>
        /// <see cref="ProgressStagePoint"/> is checked.
        /// </summary>
        Tick
    }
}