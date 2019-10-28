/// <summary>
/// Bit Invasion namespace
/// </summary>
namespace BitInvasion
{
    /// <summary>
    /// Game state enumerator
    /// </summary>
    public enum EGameState
    {
        /// <summary>
        /// Nothing
        /// </summary>
        Nothing,

        /// <summary>
        /// Waiting for input
        /// </summary>
        WaitingForInput,

        /// <summary>
        /// Playing
        /// </summary>
        Playing,

        /// <summary>
        /// Pausing
        /// </summary>
        Pausing,

        /// <summary>
        /// Won game
        /// </summary>
        WonGame,

        /// <summary>
        /// Lost game
        /// </summary>
        LostGame,

        /// <summary>
        /// Finished
        /// </summary>
        Finished
    }
}
