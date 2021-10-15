
    /// <summary>
    /// Модель результатів пройденого рівня
    /// </summary>
    public struct Results
    {
        /// <summary>
        /// Quantity of earned <see cref="Utilities.SaveLoadData.Painting.pieces"/>.
        /// </summary>
        public int ImagePiecesCount { get; set; }
        
        /// <summary>
        /// Percentage of painted papers
        /// </summary>
        public float PaperPaintedRatio { get; set; }
    
        /// <summary>
        /// Quantity of drops.
        /// </summary>
        public int InkDropsCount { get; set; }
        
    }
    
    
