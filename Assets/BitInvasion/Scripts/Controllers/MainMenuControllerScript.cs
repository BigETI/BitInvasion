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
        /// Start game
        /// </summary>
        public void StartGame()
        {
            SceneLoaderManager.LoadScene("GameScene");
        }

        /// <summary>
        /// Show options
        /// </summary>
        public void ShowOptions()
        {
            SceneLoaderManager.LoadScene("OptionsScene");
        }

        /// <summary>
        /// Exit game
        /// </summary>
        public void ExitGame()
        {
            Game.Quit();
        }
    }
}
