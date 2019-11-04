using UnityEngine;
using UnitySceneLoaderManager;
using UnityUtils;

/// <summary>
/// Intro controller script
/// </summary>
public class IntroControllerScript : MonoBehaviour
{
    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (Game.AnyKeyDown)
        {
            SceneLoaderManager.LoadScene("MainMenuScene");
        }
    }
}
