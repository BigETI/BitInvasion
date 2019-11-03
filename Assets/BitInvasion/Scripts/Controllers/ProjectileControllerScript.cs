using UnityEngine;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Projectile controller script class
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Speed
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float speed = 6.0f;

        /// <summary>
        /// Damage
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float damage = 20.0f;

        /// <summary>
        /// Spaceship hit camera shake intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float spaceshipHitCameraShakeIntensity = 0.5f;

        /// <summary>
        /// Barrier hit camera shake intensity
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float barrierHitCameraShakeIntensity = 0.5f;

        /// <summary>
        /// Camera shake time in seconds
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float cameraShakeTime = 0.25f;

        /// <summary>
        /// Despawn time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float despawnTime = 3.0f;

        /// <summary>
        /// Explosion particle system lifetime in seconds
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float explosionParticleSystemLifetime = 3.0f;

        /// <summary>
        /// Explosion particle system asset
        /// </summary>
        [SerializeField]
        private GameObject explosionParticleSystemAsset = default;

        /// <summary>
        /// Projectile rigidbody
        /// </summary>
        private Rigidbody projectileRigidbody;

        /// <summary>
        /// Camera controller
        /// </summary>
        private CameraControllerScript cameraController;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            projectileRigidbody = GetComponent<Rigidbody>();
            if (projectileRigidbody != null)
            {
                projectileRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                projectileRigidbody.velocity = transform.up * speed;
                projectileRigidbody.useGravity = false;
            }
            cameraController = FindObjectOfType<CameraControllerScript>();
            Destroy(gameObject, despawnTime);
        }

        /// <summary>
        /// On collision enter
        /// </summary>
        /// <param name="collision">Collision</param>
        private void OnCollisionEnter(Collision collision)
        {
            float camera_shake_intensity = 0.0f;
            SpaceshipControllerScript spaceship_controller = collision.gameObject.GetComponent<SpaceshipControllerScript>();
            BarrierControllerScript barrier_controller = collision.gameObject.GetComponent<BarrierControllerScript>();
            if (spaceship_controller != null)
            {
                spaceship_controller.Damage(damage);
                camera_shake_intensity += spaceshipHitCameraShakeIntensity;
            }
            if (barrier_controller != null)
            {
                Destroy(barrier_controller.gameObject);
                camera_shake_intensity += barrierHitCameraShakeIntensity;
            }
            if (explosionParticleSystemAsset != null)
            {
                GameObject go = Instantiate(explosionParticleSystemAsset, transform.position, transform.rotation);
                if (go != null)
                {
                    Destroy(go, explosionParticleSystemLifetime);
                }
            }
            cameraController?.ShakeCamera(camera_shake_intensity, cameraShakeTime);
            Destroy(gameObject);
        }
    }
}
