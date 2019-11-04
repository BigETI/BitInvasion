using BitInvasion.Data;
using UnityAudioManager;
using UnityEngine;
using UnitySaveGame;

/// <summary>
/// Bit Invasion triggers namespace
/// </summary>
namespace BitInvasion.Triggers
{
    /// <summary>
    /// Audio options trigger script class
    /// </summary>
    public class AudioOptionsTriggerScript : MonoBehaviour
    {
        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            SaveGameData save_game_data = SaveGame.GetData<SaveGameData>();
            if (save_game_data != null)
            {
                AudioManager.MusicVolume = save_game_data.MusicVolume;
                AudioManager.SoundEffectsVolume = save_game_data.SoundEffectsVolume;
            }
            Destroy(gameObject);
        }
    }
}
