using BitInvasion.Managers;

/// <summary>
/// Bit Invasion namespace
/// </summary>
namespace BitInvasion
{
    /// <summary>
    /// Game manager class
    /// </summary>
    public static class GameManager
    {
        /// <summary>
        /// Game state
        /// </summary>
        public static EGameState GameState => ((GameManagerScript.Instance == null) ? EGameState.Nothing : GameManagerScript.Instance.GameState);

        /// <summary>
        /// Score
        /// </summary>
        public static long Score
        {
            get => ((GameManagerScript.Instance == null) ? 0L : GameManagerScript.Instance.Score);
            set
            {
                if (GameManagerScript.Instance != null)
                {
                    GameManagerScript.Instance.Score = value;
                }
            }
        }
    }
}
