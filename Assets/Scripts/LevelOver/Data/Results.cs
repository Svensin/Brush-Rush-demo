
    /// <summary>
    /// Модель результатів пройденого рівня
    /// </summary>
    public struct Results
    {
        /// <summary>
        /// К-ть отриманих <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
        /// </summary>
        public int ImagePiecesCount { get; set; }
        
        /// <summary>
        /// Відсоток пройденого паперу.
        /// </summary>
        public float PaperPaintedRatio { get; set; }
    
        /// <summary>
        /// К-ть крапель.
        /// </summary>
        public int InkDropsCount { get; set; }
        
    }
    
    
