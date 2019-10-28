using UnityEngine;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Camera controller script class
    /// </summary>
    public class CameraControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Roughness
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float roughness = 0.75f;

        /// <summary>
        /// Camera offset
        /// </summary>
        [SerializeField]
        private Vector2 cameraOffset = new Vector2(16.0f, 8.0f);

        /// <summary>
        /// Camera shake movement
        /// </summary>
        [SerializeField]
        private Vector3 cameraShakeMovement = Vector3.one;

        /// <summary>
        /// Camera shake curve
        /// </summary>
        [SerializeField]
        private AnimationCurve cameraShakeCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 0.0f);

        /// <summary>
        /// Use fixed update
        /// </summary>
        [SerializeField]
        private bool useFixedUpdate = true;

        /// <summary>
        /// Follow spaceship controller
        /// </summary>
        [SerializeField]
        private SpaceshipControllerScript followSpaceshipController;

        /// <summary>
        /// Camera shake intensity
        /// </summary>
        private float cameraShakeIntensity = default;

        /// <summary>
        /// Camera shake time in seconds
        /// </summary>
        private float cameraShakeTime = default;

        /// <summary>
        /// Camera shake elapsed time in seconds
        /// </summary>
        private float cameraShakeElapsedTime = default;

        /// <summary>
        /// Camera shake intensity
        /// </summary>
        private float CameraShakeIntensity => ((cameraShakeCurve == null) ? ((cameraShakeTime > float.Epsilon) ? (Mathf.Clamp(cameraShakeElapsedTime / cameraShakeTime, 0.0f, 1.0f) * cameraShakeIntensity) : 0.0f) : (cameraShakeCurve.Evaluate((cameraShakeTime > float.Epsilon) ? Mathf.Clamp(cameraShakeElapsedTime / cameraShakeTime, 0.0f, 1.0f) : 1.0f) * cameraShakeIntensity));

        /// <summary>
        /// Shake camera
        /// </summary>
        /// <param name="intensity">Intensity</param>
        /// <param name="time">Time in seconds</param>
        public void ShakeCamera(float intensity, float time)
        {
            cameraShakeIntensity = Mathf.Max(intensity, 0.0f);
            cameraShakeTime = Mathf.Max(time, 0.0f);
            cameraShakeElapsedTime = 0.0f;
        }

        /// <summary>
        /// Update camera
        /// </summary>
        /// <param name="isFixedUpdate">Is called from fixed update</param>
        private void UpdateCamera(bool isFixedUpdate)
        {
            if (followSpaceshipController != null)
            {
                if (cameraShakeElapsedTime < cameraShakeTime)
                {
                    cameraShakeElapsedTime += (isFixedUpdate ? Time.fixedDeltaTime : Time.deltaTime);
                    if (cameraShakeElapsedTime > cameraShakeTime)
                    {
                        cameraShakeElapsedTime = cameraShakeTime;
                    }
                }
                Vector2 direction = (new Vector2(followSpaceshipController.Direction.x, 1.0f)).normalized;
                direction.y = 1.0f;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((followSpaceshipController.CharacterPosition - transform.position) + ((direction.sqrMagnitude > 0.0625f) ? ((followSpaceshipController.transform.right * direction.x * cameraOffset.x) + (followSpaceshipController.transform.up * direction.y * cameraOffset.y)) : (Vector3.up * cameraOffset.y)), Vector3.up), roughness);
                float camera_shake_intensity = CameraShakeIntensity;
                transform.localPosition = new Vector3(cameraShakeMovement.x * Random.Range(0.0f, 1.0f) * camera_shake_intensity, cameraShakeMovement.y * Random.Range(0.0f, 1.0f) * camera_shake_intensity, cameraShakeMovement.z * Random.Range(0.0f, 1.0f) * camera_shake_intensity);
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            if (followSpaceshipController == null)
            {
                PlayerControllerScript player_controller = FindObjectOfType<PlayerControllerScript>();
                if (player_controller != null)
                {
                    followSpaceshipController = player_controller.GetComponent<SpaceshipControllerScript>();
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (!useFixedUpdate)
            {
                UpdateCamera(useFixedUpdate);
            }
        }

        /// <summary>
        /// Fixed update
        /// </summary>
        private void FixedUpdate()
        {
            if (useFixedUpdate)
            {
                UpdateCamera(useFixedUpdate);
            }
        }
    }
}
