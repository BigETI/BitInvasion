using UnityEngine;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// AI controller script class
    /// </summary>
    [RequireComponent(typeof(SpaceshipControllerScript))]
    public class AIControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Detection angle
        /// </summary>
        [SerializeField]
        [Range(0.0f, 360.0f)]
        private float detectionAngle = 12.5f;

        /// <summary>
        /// Sprint when less or equal than value
        /// </summary>
        [SerializeField]
        private uint sprintWhenLessEquals = 5U;

        /// <summary>
        /// Direction
        /// </summary>
        [SerializeField]
        private Vector2 direction = Vector2.left;

        /// <summary>
        /// Spaceship controller
        /// </summary>
        private SpaceshipControllerScript spaceshipController;

        /// <summary>
        /// Swarm controller
        /// </summary>
        private SwarmControllerScript swarmController;

        /// <summary>
        /// Target transformation
        /// </summary>
        private Transform targetTransform;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            spaceshipController = GetComponent<SpaceshipControllerScript>();
            swarmController = FindObjectOfType<SwarmControllerScript>();
            PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
            if (player_controller != null)
            {
                targetTransform = player_controller.transform;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (spaceshipController != null)
            {
                spaceshipController.Direction = direction;
                spaceshipController.IsShooting = ((targetTransform == null) ? false : ((Vector3.Angle(targetTransform.forward, transform.forward) <= detectionAngle) && ((swarmController == null) ? true : swarmController.IsAtBottom(gameObject))));
                spaceshipController.IsSprinting = ((swarmController == null) ? true : (swarmController.Quantity <= sprintWhenLessEquals));
            }
        }
    }
}
