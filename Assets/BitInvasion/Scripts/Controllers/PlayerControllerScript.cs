using UnityEngine;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Player controller script class
    /// </summary>
    [RequireComponent(typeof(SpaceshipControllerScript))]
    public class PlayerControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Spaceship controller
        /// </summary>
        private SpaceshipControllerScript spaceshipController;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            spaceshipController = GetComponent<SpaceshipControllerScript>();
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (spaceshipController != null)
            {
                spaceshipController.Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                spaceshipController.IsSprinting = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
                spaceshipController.IsShooting = Input.GetKey(KeyCode.Space);
            }
        }
    }
}
