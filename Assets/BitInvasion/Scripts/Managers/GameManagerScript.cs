using BitInvasion.Controllers;
using UnityEngine;
using UnityEngine.Events;
using UnitySceneLoaderManager;

/// <summary>
/// Bit Invasion managers namespace
/// </summary>
namespace BitInvasion.Managers
{
    /// <summary>
    /// Game manager script class
    /// </summary>
    public class GameManagerScript : MonoBehaviour
    {
        /// <summary>
        /// HUD panel controller
        /// </summary>
        [SerializeField]
        private HUDPanelControllerScript hudPanelController;

        /// <summary>
        /// On waiting for input
        /// </summary>
        [SerializeField]
        private UnityEvent onWaitingInput = default;

        /// <summary>
        /// On game started
        /// </summary>
        [SerializeField]
        private UnityEvent onGameStarted = default;

        /// <summary>
        /// On game paused
        /// </summary>
        [SerializeField]
        private UnityEvent onGamePaused = default;

        /// <summary>
        /// On game resumed
        /// </summary>
        [SerializeField]
        private UnityEvent onGameResumed = default;

        /// <summary>
        /// On game won
        /// </summary>
        [SerializeField]
        private UnityEvent onGameWon = default;

        /// <summary>
        /// On game lost
        /// </summary>
        [SerializeField]
        private UnityEvent onGameLost = default;

        /// <summary>
        /// On game finished
        /// </summary>
        [SerializeField]
        private UnityEvent onGameFinished = default;

        /// <summary>
        /// Game state
        /// </summary>
        private EGameState gameState;

        /// <summary>
        /// Score
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// Game state
        /// </summary>
        public EGameState GameState
        {
            get => gameState;
            set
            {
                if (gameState != value)
                {
                    switch (gameState)
                    {
                        case EGameState.Nothing:
                            if (value == EGameState.WaitingForInput)
                            {
                                gameState = EGameState.WaitingForInput;
                                onWaitingInput?.Invoke();
                            }
                            break;
                        case EGameState.WaitingForInput:
                            if (value == EGameState.Playing)
                            {
                                gameState = EGameState.Playing;
                                onGameStarted?.Invoke();
                            }
                            break;
                        case EGameState.Playing:
                            switch (value)
                            {
                                case EGameState.Pausing:
                                    gameState = EGameState.Pausing;
                                    onGamePaused?.Invoke();
                                    break;
                                case EGameState.WonGame:
                                    gameState = EGameState.WonGame;
                                    onGameWon?.Invoke();
                                    break;
                                case EGameState.LostGame:
                                    gameState = EGameState.LostGame;
                                    onGameLost?.Invoke();
                                    break;
                            }
                            break;
                        case EGameState.Pausing:
                            if (value == EGameState.Playing)
                            {
                                gameState = EGameState.Playing;
                                onGameResumed?.Invoke();
                            }
                            break;
                        case EGameState.WonGame:
                        case EGameState.LostGame:
                            gameState = EGameState.Finished;
                            onGameFinished?.Invoke();
                            break;
                    }
                    Time.timeScale = ((gameState == EGameState.Playing) ? 1.0f : 0.0f);
                }
            }
        }

        /// <summary>
        /// Instance
        /// </summary>
        public static GameManagerScript Instance { get; private set; }

        /// <summary>
        /// Resume game
        /// </summary>
        public void ResumeGame()
        {
            GameState = EGameState.Playing;
        }

        /// <summary>
        /// Restart game
        /// </summary>
        public void RestartGame()
        {
            Time.timeScale = 1.0f;
            SceneLoaderManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame()
        {
            Time.timeScale = 1.0f;
            SceneLoaderManager.LoadScene("MainMenuScene");
        }

        /// <summary>
        /// Loose game
        /// </summary>
        public void LooseGame()
        {
            GameState = EGameState.LostGame;
        }

        /// <summary>
        /// Win game
        /// </summary>
        public void WinGame()
        {
            GameState = EGameState.WonGame;
        }

        /// <summary>
        /// On enable
        /// </summary>
        private void OnEnable()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        /// <summary>
        /// On disable
        /// </summary>
        private void OnDisable()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            if (hudPanelController == null)
            {
                hudPanelController = FindObjectOfType<HUDPanelControllerScript>();
            }
            GameState = EGameState.WaitingForInput;
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (gameState)
                {
                    case EGameState.Playing:
                        GameState = EGameState.Pausing;
                        break;
                    case EGameState.Pausing:
                        GameState = EGameState.Playing;
                        break;
                }
            }
            else if (Input.anyKeyDown)
            {
                switch (gameState)
                {
                    case EGameState.WaitingForInput:
                        GameState = EGameState.Playing;
                        break;
                    case EGameState.WonGame:
                    case EGameState.LostGame:
                        GameState = EGameState.Finished;
                        break;
                }
            }
            if (hudPanelController != null)
            {
                hudPanelController.Score = Score;
            }
        }
    }
}
