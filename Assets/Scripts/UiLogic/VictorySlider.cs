using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UiLogic
{
    /// <summary>
    /// Progress bar after level ending <see cref="CompleteLevelMenu"/>.
    /// </summary>
    public class VictorySlider : MonoBehaviour
    {
        /// <summary>
        /// Slider of the bar
        /// </summary>
        [SerializeField] private Image sliderBar;

        public Image SliderBar => sliderBar;
        
        /// <summary>
        /// <see cref="List"/> list of  stage for receiving <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
        /// </summary>
        [SerializeField]private List<ProgressStagePoint> _stagePoints;

        public List<ProgressStagePoint> StagePoints => _stagePoints;
        
        /// <summary>
        /// Min quantity <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
        /// </summary>
        [Min(1)] [SerializeField] private int minPiecesQuantity;

        public int MinPiecesQuantity => minPiecesQuantity;

        /// <summary>
        /// Fist stage
        /// </summary>
        public ProgressStagePoint FirstStagePoint => _stagePoints[0];

        /// <summary>
        /// Enumerator for current stage <see cref="_stagePoints"/>.
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