using UnityEngine;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Turret controller script class
    /// </summary>
    public class TurretControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Shoot time
        /// </summary>
        [SerializeField]
        [Range(0.0f, 100.0f)]
        private float shootTime = 0.125f;

        /// <summary>
        /// Projectile asset
        /// </summary>
        [SerializeField]
        private GameObject projectileAsset = default;

        /// <summary>
        /// Elapsed time
        /// </summary>
        private float elapsedTime = default;

        /// <summary>
        /// Shoot
        /// </summary>
        public void Shoot()
        {
            if ((projectileAsset) && (elapsedTime >= shootTime))
            {
                elapsedTime = 0.0f;
                Instantiate(projectileAsset, transform.position, transform.rotation);
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (elapsedTime < shootTime)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime > shootTime)
                {
                    elapsedTime = shootTime;
                }
            }
        }

        /// <summary>
        /// On draw gizmos
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 0.25f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (transform.up * 0.5f));
        }
    }
}
