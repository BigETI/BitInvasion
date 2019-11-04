using UnityEngine;
using UnitySaveGame;

/// <summary>
/// Bit Invasion data namespace
/// </summary>
namespace BitInvasion.Data
{
    /// <summary>
    /// Save game data class
    /// </summary>
    public class SaveGameData : ASaveGameData
    {
        /// <summary>
        /// Music volume
        /// </summary>
        [SerializeField]
        private float musicVolume = 0.35f;

        /// <summary>
        /// Sound effects volume
        /// </summary>
        [SerializeField]
        private float soundEffectsVolume = 1.0f;

        /// <summary>
        /// Music volume
        /// </summary>
        public float MusicVolume
        {
            get => Mathf.Clamp(musicVolume, 0.0f, 1.0f);
            set => musicVolume = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Sound effects volume
        /// </summary>
        public float SoundEffectsVolume
        {
            get => Mathf.Clamp(soundEffectsVolume, 0.0f, 1.0f);
            set => soundEffectsVolume = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SaveGameData() : base(null)
        {
            // ...
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="saveGameData"></param>
        public SaveGameData(ASaveGameData saveGameData) : base(saveGameData)
        {
            if (saveGameData is SaveGameData)
            {
                SaveGameData save_game_data = (SaveGameData)saveGameData;
                MusicVolume = save_game_data.MusicVolume;
                SoundEffectsVolume = save_game_data.SoundEffectsVolume;
            }
        }
    }
}
