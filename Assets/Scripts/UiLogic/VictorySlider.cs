using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UiLogic
{
    /// <summary>
    /// Шкала прогресу після закінчення рівня для <see cref="CompleteLevelMenu"/>.
    /// </summary>
    public class VictorySlider : MonoBehaviour
    {
        /// <summary>
        /// Слайдер бар
        /// </summary>
        [SerializeField] private Image sliderBar;

        public Image SliderBar => sliderBar;
        
        /// <summary>
        /// <see cref="List"/> - список стадій для отримання <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
        /// </summary>
        [SerializeField]private List<ProgressStagePoint> _stagePoints;

        public List<ProgressStagePoint> StagePoints => _stagePoints;
        
        /// <summary>
        /// Мінімальна к-ть <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
        /// </summary>
        [Min(1)] [SerializeField] private int minPiecesQuantity;

        public int MinPiecesQuantity => minPiecesQuantity;

        /// <summary>
        /// Перша стадія
        /// </summary>
        public ProgressStagePoint FirstStagePoint => _stagePoints[0];

        /// <summary>
        /// Енумератор на поточну стадію <see cref="_stagePoints"/>.
        /// </summary>
        public IEnumerator<ProgressStagePoint> CurrentStagePoint => _stagePoints.GetEnumerator();

        private void Start()
        {
            foreach (var point in _stagePoints)
            {
                point.ChangeStageText(minPiecesQuantity);
            }
        }
    }
}