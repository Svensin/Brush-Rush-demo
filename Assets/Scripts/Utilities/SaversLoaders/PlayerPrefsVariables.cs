namespace Utilities.SaversLoaders
{
    /// <summary>
    /// Contains names of player prefs variables
    /// </summary>
    public static class PlayerPrefsVariables
    {
        private const string sounds = "sounds";
        private const string music = "music";
        private const string vibrations = "vibration";
        private const string currentLevel = "current_level";
        private const string currentPaintingName = "current_painting";

        public static string CURRENT_PAINTING_NAME => currentPaintingName;
        public static string CURRENT_LEVEL => currentLevel;
        public static string SOUNDS => sounds;
        public static string MUSIC => music;
        public static string VIBRATIONS => vibrations;
    }
}