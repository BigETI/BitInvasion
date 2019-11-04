using System;
using UnityAudioManager;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Spaceship controller script class
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class SpaceshipControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Health
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float health = 100.0f;

        /// <summary>
        /// Score
        /// </summary>
        [SerializeField]
        private long score = 1000L;

        /// <summary>
        /// Speed
        /// </summary>
        [SerializeField]
        private Vector2 speed = new Vector2(1.0f, 10.0f);

        /// <summary>
        /// Sprint multiplier
        /// </summary>
        [SerializeField]
        private Vector2 sprintMultiplier = new Vector2(2.0f, 2.0f);

        /// <summary>
        /// Deadzone
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1.0f)]
        private float deadzone = 0.0625f;

        /// <summary>
        /// Explosion particle system lifetime in seconds
        /// </summary>
        [SerializeField]
        [Range(0.0f, 1000.0f)]
        private float explosionParticleSystemLifetime = 3.0f;

        /// <summary>
        /// Character transform
        /// </summary>
        [SerializeField]
        private Transform characterTransform = default;

        /// <summary>
        /// Explosion particle system asset
        /// </summary>
        [SerializeField]
        private GameObject explosionParticleSystemAsset = default;

        /// <summary>
        /// Explosion audio clip
        /// </summary>
        [SerializeField]
        private AudioClip explosionAudioClip = default;

        /// <summary>
        /// Turret controllers
        /// </summary>
        [SerializeField]
        private TurretControllerScript[] turretControllers;

        /// <summary>
        /// On death
        /// </summary>
        [SerializeField]
        private UnityEvent onDeath = default;

        /// <summary>
        /// Spaceship rigidbody
        /// </summary>
        private Rigidbody spaceshipRigidbody;

        /// <summary>
        /// Direction
        /// </summary>
        private Vector2 direction;

        /// <summary>
        /// Health
        /// </summary>
        public float Health => health;

        /// <summary>
        /// Turret controllers
        /// </summary>
        private TurretControllerScript[] TurretControllers
        {
            get
            {
                if (turretControllers == null)
                {
                    turretControllers = Array.Empty<TurretControllerScript>();
                }
                return turretControllers;
            }
        }

        /// <summary>
        /// Direction
        /// </summary>
        public Vector2 Direction
        {
            get => direction;
            set
            {
                direction = value;
                if (direction.sqrMagnitude > 1.0f)
                {
                    direction.Normalize();
                }
            }
        }

        /// <summary>
        /// Is shooting
        /// </summary>
        public bool IsShooting { get; set; }

        /// <summary>
        /// Is sprinting
        /// </summary>
        public bool IsSprinting { get; set; }

        /// <summary>
        /// Character position
        /// </summary>
        public Vector3 CharacterPosition => ((characterTransform == null) ? transform.position : characterTransform.position);

        /// <summary>
        /// Damage spaceship
        /// </summary>
        /// <param name="amount">Amount</param>
        public void Damage(float amount)
        {
            if (health > float.Epsilon)
            {
                health -= Mathf.Abs(amount);
                if (health <= float.Epsilon)
                {
                    health = 0.0f;
                    GameManager.Score += score;
                    if (explosionParticleSystemAsset != null)
                    {
                        GameObject go = Instantiate(explosionParticleSystemAsset, CharacterPosition, transform.rotation);
                        if (go != null)
                        {
                            Destroy(go, explosionParticleSystemLifetime);
                        }
                    }
                    AudioManager.PlaySoundEffect(explosionAudioClip);
                    onDeath?.Invoke();
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            spaceshipRigidbody = GetComponent<Rigidbody>();
            if (spaceshipRigidbody != null)
            {
                spaceshipRigidbody.constraints = RigidbodyConstraints.FreezeAll & (~(RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY));
                spaceshipRigidbody.centerOfMass = Vector3.zero;
                spaceshipRigidbody.useGravity = false;
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (spaceshipRigidbody != null)
            {
                if (direction.sqrMagnitude > (deadzone * deadzone))
                {
                    spaceshipRigidbody.velocity = transform.up * direction.y * speed.y * (IsSprinting ? sprintMultiplier.y : 1.0f);
                    spaceshipRigidbody.angularVelocity = transform.up * direction.x * speed.x * (IsSprinting ? sprintMultiplier.x : 1.0f);
                }
                if (IsShooting)
                {
                    foreach (TurretControllerScript turret_controller in TurretControllers)
                    {
                        turret_controller?.Shoot();
                    }
                }
            }
        }

        /// <summary>
        /// On collision enter
        /// </summary>
        /// <param name="collision">Collision</param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<BarrierControllerScript>() != null)
            {
                Destroy(collision.gameObject);
            }
        }

        /// <summary>
        /// On draw gizmos selected
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(CharacterPosition, 3.0f);
        }
    }
}
