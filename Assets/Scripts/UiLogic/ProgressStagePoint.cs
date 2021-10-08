using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    /// <summary>
    /// Стадія для видачі <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
    /// </summary>
    public class ProgressStagePoint : MonoBehaviour
    {
        /// <summary>
        /// Номер стадії
        /// </summary>
        [Range(0, 5)] [SerializeField] private int stageCount;
        
        /// <summary>
        /// Об'єкт-зображення "галочки"
        /// </summary>
        [SerializeField] private GameObject tickImage;
        /// <summary>
        /// Об'єкт-зображення "хрестика"
        /// </summary>
        [SerializeField] private GameObject crossImage;
        /// <summary>
        /// <see cref="Text.text"/>, де відображається к-ть <see cref="Utilities.SaveLoadData.Painting.pieces"/> за стадію.
        /// </summary>
        [SerializeField] private Text stageText;

        /// <summary>
        /// Мінімальні-максимальні значення <see cref="Results.PaperPaintedRatio"/> у які входить стадія.
        /// </summary>
        [Header("Stage values")]
        [SerializeField] private Vector2 minMaxStageValues;
        /// <summary>
        /// Поточне значення <see cref="Results.PaperPaintedRatio"/> для стадії.
        /// </summary>
        [Range(0,1f)][SerializeField] private float stageValue;
        
        public float StageValue => stageValue;

        /// <summary>
        /// Стан даної стадії.
        /// </summary>
        public StageState State { get; private set; }

        /// <summary>
        /// Змінює <see cref="Text.text"/> для стадії.
        /// </summary>
        /// <param name="piecesCount">к-ть <see cref="Utilities.SaveLoadData.Painting.pieces"/></param>
        public void ChangeStageText(int piecesCount = 0)
        {
            stageText.text = $"{piecesCount + stageCount}x";
        }

        /// <summary>
        /// Змінює <see cref="StageState"/> стадії
        /// </summary>
        /// <param name="state"><see cref="StageState"/> стадії</param>
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
    /// Стан <see cref="ProgressStagePoint"/>.
    /// </summary>
    public enum StageState
    {
        /// <summary>
        /// <see cref="ProgressStagePoint"/> не пройдена.
        /// </summary>
        Cross,
        /// <summary>
        /// <see cref="ProgressStagePoint"/> пройдена.
        /// </summary>
        Tick
    }
}