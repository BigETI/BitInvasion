using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// HUD panel controller script class
    /// </summary>
    public class HUDPanelControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Default score format
        /// </summary>
        private static readonly string defaultScoreFormat = "Score: {0}";

        /// <summary>
        /// Default health format
        /// </summary>
        private static readonly string defaultHealthFormat = "{0}%";

        /// <summary>
        /// Maximum health fill color
        /// </summary>
        [SerializeField]
        private Color maxHealthFillColor = Color.white;

        /// <summary>
        /// Minimum health fill color
        /// </summary>
        [SerializeField]
        private Color minHealthFillColor = Color.white;

        /// <summary>
        /// Maximum health background color
        /// </summary>
        [SerializeField]
        private Color maxHealthBackgroundColor = Color.white;

        /// <summary>
        /// Minimum health background color
        /// </summary>
        [SerializeField]
        private Color minHealthBackgroundColor = Color.white;

        /// <summary>
        /// Score per second
        /// </summary>
        [SerializeField]
        private float scorePerSecond = 4000.0f;

        /// <summary>
        /// Score format
        /// </summary>
        [SerializeField]
        private string scoreFormat = defaultScoreFormat;

        /// <summary>
        /// Health per second
        /// </summary>
        [SerializeField]
        private float healthPerSecond = 50.0f;

        /// <summary>
        /// Health format
        /// </summary>
        [SerializeField]
        private string healthFormat = defaultHealthFormat;

        /// <summary>
        /// Score text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI scoreText = default;

        /// <summary>
        /// Health text
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI healthText = default;

        /// <summary>
        /// Health slider
        /// </summary>
        [SerializeField]
        private Slider healthSlider = default;

        /// <summary>
        /// Health slider fill image
        /// </summary>
        [SerializeField]
        private Image healthSliderFillImage = default;

        /// <summary>
        /// Health slider background image
        /// </summary>
        [SerializeField]
        private Image healthSliderBackgroundImage = default;

        /// <summary>
        /// Spaceship controller
        /// </summary>
        private SpaceshipControllerScript spaceshipController;

        /// <summary>
        /// Display score
        /// </summary>
        private long displayScore;

        /// <summary>
        /// Display health
        /// </summary>
        private float displayHealth;

        /// <summary>
        /// Score
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// Health
        /// </summary>
        public float Health { get; set; }

        /// <summary>
        /// Score format
        /// </summary>
        private string ScoreFormat
        {
            get
            {
                if (scoreFormat == null)
                {
                    scoreFormat = defaultScoreFormat;
                }
                return scoreFormat;
            }
        }

        /// <summary>
        /// Health format
        /// </summary>
        private string HealthFormat
        {
            get
            {
                if (healthFormat == null)
                {
                    healthFormat = defaultHealthFormat;
                }
                return healthFormat;
            }
        }

        /// <summary>
        /// Update HUD
        /// </summary>
        private void UpdateHUD()
        {
            float health_value = Mathf.Clamp(displayHealth / 100.0f, 0.0f, 1.0f);
            if (scoreText != null)
            {
                scoreText.text = string.Format(ScoreFormat, displayScore);
            }
            if (healthText != null)
            {
                healthText.text = string.Format(HealthFormat, Mathf.RoundToInt(displayHealth));
            }
            if (healthSlider != null)
            {
                healthSlider.value = health_value;
            }
            if (healthSliderFillImage != null)
            {
                healthSliderFillImage.color = Color.Lerp(minHealthFillColor, maxHealthFillColor, health_value);
            }
            if (healthSliderBackgroundImage != null)
            {
                healthSliderBackgroundImage.color = Color.Lerp(minHealthBackgroundColor, maxHealthBackgroundColor, health_value);
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller != null)
            {
                spaceshipController = player_controller.GetComponent<SpaceshipControllerScript>();
                Health = ((spaceshipController == null) ? 0.0f : spaceshipController.Health);
            }
            UpdateHUD();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            bool update_hud = false;
            Health = ((spaceshipController == null) ? 0.0f : spaceshipController.Health);
            if (displayScore != Score)
            {
                long score = Mathf.RoundToInt(Time.unscaledDeltaTime * scorePerSecond);
                if (displayScore < Score)
                {
                    displayScore += score;
                    if (displayScore > Score)
                    {
                        displayScore = Score;
                    }
                }
                else
                {
                    displayScore -= score;
                    if (displayScore < Score)
                    {
                        displayScore = Score;
                    }
                }
                update_hud = true;
            }
            if (displayHealth != Health)
            {
                float health = Time.unscaledDeltaTime * healthPerSecond;
                if (displayHealth < Health)
                {
                    displayHealth += health;
                    if (displayHealth > Health)
                    {
                        displayHealth = Health;
                    }
                }
                else
                {
                    displayHealth -= health;
                    if (displayHealth < Health)
                    {
                        displayHealth = Health;
                    }
                }
                update_hud = true;
            }
            if (update_hud)
            {
                UpdateHUD();
            }
        }
    }
}
