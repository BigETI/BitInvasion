using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Bit Invasion controllers namespace
/// </summary>
namespace BitInvasion.Controllers
{
    /// <summary>
    /// Swarm controller script class
    /// </summary>
    public class SwarmControllerScript : MonoBehaviour
    {
        /// <summary>
        /// Rows
        /// </summary>
        [SerializeField]
        private uint rows = 4U;

        /// <summary>
        /// Columns
        /// </summary>
        [SerializeField]
        private uint columns = 16U;

        /// <summary>
        /// Spacing
        /// </summary>
        [SerializeField]
        private Vector2 spacing = new Vector2(10.0f, 2.0f);

        /// <summary>
        /// Spaceship asset
        /// </summary>
        [SerializeField]
        private GameObject spaceshipAsset = default;

        /// <summary>
        /// On defeated
        /// </summary>
        [SerializeField]
        private UnityEvent onDefeated = default;

        /// <summary>
        /// Swarm objects
        /// </summary>
        private GameObject[,] swarmObjects;

        /// <summary>
        /// Quantity
        /// </summary>
        public uint Quantity
        {
            get
            {
                uint ret = 0U;
                if (swarmObjects != null)
                {
                    foreach (GameObject swarm_object in swarmObjects)
                    {
                        if (swarm_object != null)
                        {
                            ++ret;
                        }
                    }
                }
                return ret;
            }
        }

        /// <summary>
        /// Is at bottom
        /// </summary>
        /// <param name="swarmObject">Swarm object</param>
        /// <returns>"true" if at bottom, otherwise "false"</returns>
        public bool IsAtBottom(GameObject swarmObject)
        {
            bool ret = false;
            if ((swarmObject != null) && (swarmObjects != null))
            {
                for (int x = 0, y, x_len = swarmObjects.GetLength(0), y_len = swarmObjects.GetLength(1); x < x_len; x++)
                {
                    for (y = 0; y < y_len; y++)
                    {
                        GameObject swarm_object = swarmObjects[x, y];
                        if (swarm_object != null)
                        {
                            ret = (swarmObject == swarm_object);
                            break;
                        }
                    }
                    if (ret)
                    {
                        break;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            swarmObjects = new GameObject[columns, rows];
            int x_len = swarmObjects.GetLength(0);
            int y_len = swarmObjects.GetLength(1);
            Vector2 offset = new Vector2(Mathf.Max(x_len - 1, 0) * spacing.x * -0.5f, Mathf.Max(y_len - 1, 0) * spacing.y * -0.5f);
            for (int x, y = 0; y < y_len; y++)
            {
                for (x = 0; x < x_len; x++)
                {
                    swarmObjects[x, y] = Instantiate(spaceshipAsset, new Vector3(transform.position.x, transform.position.y + (y * spacing.y) + transform.position.z, 0.0f), Quaternion.AngleAxis((x * spacing.x) + offset.x, transform.up), transform);
                }
            }
        }

        /// <summary>
        /// Update
        /// </summary>
        private void Update()
        {
            if (swarmObjects != null)
            {
                bool defeated = true;
                for (int x, y = 0, x_len = swarmObjects.GetLength(0), y_len = swarmObjects.GetLength(1); y < y_len; y++)
                {
                    for (x = 0; x < x_len; x++)
                    {
                        if (swarmObjects[x, y] != null)
                        {
                            defeated = false;
                            break;
                        }
                    }
                }
                if (defeated)
                {
                    swarmObjects = null;
                    onDefeated?.Invoke();
                }
            }
        }
    }
}
