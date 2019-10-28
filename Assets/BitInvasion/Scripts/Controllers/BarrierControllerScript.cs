using UnityEngine;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Barrier controller script class
    /// </summary>
    public class BarrierControllerScript : MonoBehaviour
    {
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
        /// On collision enter
        /// </summary>
        /// <param name="collision">Collision</param>
        private void OnCollisionEnter(Collision collision)
        {
            if ((explosionParticleSystemAsset != null) && ((collision.gameObject.GetComponent<ProjectileControllerScript>() != null) || (collision.gameObject.GetComponent<SpaceshipControllerScript>() != null)))
            {
                GameObject go = Instantiate(explosionParticleSystemAsset, transform.position, transform.rotation);
                if (go != null)
                {
                    Destroy(go, explosionParticleSystemLifetime);
                }
            }
        }
    }
}
