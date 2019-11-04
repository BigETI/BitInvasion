using UnityEngine;
using UnitySceneLoaderManager;
using UnityUtils;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Main menu controller script class
    /// </summary>
    public class MainMenuControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Is main menu
        /// </summary>
        [SerializeField]
        private bool isMainMenu = default;

        /// <summary>
        /// Show main menu
        /// </summary>
        public void ShowMainMenu()
        {
            SceneLoaderManager.LoadScene("MainMenuScene");
        }

        /// <summary>
        /// Show options
        /// </summary>
        public void ShowOptions()
        {
            SceneLoaderManager.LoadScene("OptionsScene");
        }

        /// <summary>
        /// Start game
        /// </summary>
        public void StartGame()
        {
            SceneLoaderManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame()
        {
            Game.Quit();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isMainMenu)
                {
                    ExitGame();
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }
    }
}
