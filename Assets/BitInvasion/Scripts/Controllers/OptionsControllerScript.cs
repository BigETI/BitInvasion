using BitInvasion.Data;
using TMPro;
using UnityAudioManager;
using UnityEngine;
using UnityEngine.UI;
using UnitySaveGame;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Options controller script class
    /// </summary>
    public class OptionsControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Music volume slider
        /// </summary>
        [SerializeField]
        private Slider musicVolumeSlider = default;

        /// <summary>
        /// Music volume percent text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI musicVolumePercentText = default;

        /// <summary>
        /// Sound effects volume slider
        /// </summary>
        [SerializeField]
        private Slider soundEffectsVolumeSlider = default;

        /// <summary>
        /// Sound effects volume percent text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI soundEffectsVolumePercentText = default;

        /// <summary>
        /// Save game data
        /// </summary>
        private SaveGameData saveGameData;

        /// <summary>
        /// Music volume
        /// </summary>
        public float MusicVolume
        {
            get => ((musicVolumeSlider == null) ? 0.0f : musicVolumeSlider.value);
            set
            {
                if (musicVolumeSlider != null)
                {
                    float volume = Mathf.Clamp(value * 0.01f, 0.0f, 1.0f);
                    saveGameData.MusicVolume = volume;
                    AudioManager.MusicVolume = volume;
                    musicVolumePercentText.text = Mathf.RoundToInt(value) + "%";
                }
            }
        }

        /// <summary>
        /// Sound effects volume
        /// </summary>
        public float SoundEffectsVolume
        {
            get => ((soundEffectsVolumeSlider == null) ? 0.0f : soundEffectsVolumeSlider.value);
            set
            {
                if (soundEffectsVolumeSlider != null)
                {
                    float volume = Mathf.Clamp(value * 0.01f, 0.0f, 1.0f);
                    saveGameData.SoundEffectsVolume = volume;
                    AudioManager.SoundEffectsVolume = volume;
                    soundEffectsVolumePercentText.text = Mathf.RoundToInt(value) + "%";
                }
            }
        }

        /// <summary>
        /// Save options
        /// </summary>
        public void SaveOptions()
        {
            SaveGame.Save();
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            saveGameData = SaveGame.GetData<SaveGameData>();
            if (saveGameData != null)
            {
                if (musicVolumeSlider != null)
                {
                    musicVolumeSlider.value = AudioManager.MusicVolume * 100.0f;
                }
                if (soundEffectsVolumeSlider != null)
                {
                    soundEffectsVolumeSlider.value = AudioManager.SoundEffectsVolume * 100.0f;
                }
            }
        }
    }
}
