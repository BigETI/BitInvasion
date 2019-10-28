using UnityEngine;

/// <summary>
/// Bit Invasions managers namespace
/// </summary>
namespace BitInvasion.Managers
{
    /// <summary>
    /// Audio manager script class
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class AudioManagerScript : MonoBehaviour
    {
        /// <summary>
        /// Music audio source
        /// </summary>
        private AudioSource musicAudioSource;

        /// <summary>
        /// Instance
        /// </summary>
        public static AudioManagerScript Instance { get; private set; }

        /// <summary>
        /// Awake
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            musicAudioSource = GetComponent<AudioSource>();
            if (musicAudioSource != null)
            {
                musicAudioSource.Play();
            }
        }
    }
}
