using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PacmanAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Camera playerCamera;
    public float moveSpeed = 7f;

    private NavMeshAgent agent;
    private AudioSource audioSource;
    private Collider pacmanCollider;

    // Tracks whether Pac-Man was visible last frame.
    private bool wasVisible = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        pacmanCollider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = PlayerPrefs.GetFloat("PacmanSpeed", 5f);
    }

    void Update()
    {
        // Chase the player.
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

        // Check if Pac-Man has just become visible.
        CheckVisibility();

        // Restart the scene if Pac-Man catches the player.
        if (player != null &&
            Vector3.Distance(transform.position, player.position) < 1f)
        {
            audioSource.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void CheckVisibility()
    {
        if (playerCamera == null || audioSource == null || pacmanCollider == null)
            return;

        // Is Pac-Man inside the camera's view?
        Vector3 viewport = playerCamera.WorldToViewportPoint(transform.position);

        bool inView =
            viewport.z > 0 &&
            viewport.x >= 0 &&
            viewport.x <= 1 &&
            viewport.y >= 0 &&
            viewport.y <= 1;

        bool visible = false;

        if (inView)
        {
            Vector3 origin = playerCamera.transform.position;
            Vector3 target = pacmanCollider.bounds.center;

            Vector3 direction = target - origin;
            float distance = direction.magnitude;

            Ray ray = new Ray(origin, direction.normalized);

            RaycastHit[] hits = Physics.RaycastAll(ray, distance);

            // Sort hits from nearest to farthest.
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (RaycastHit hit in hits)
            {
                // Ignore the player's own collider.
                if (player != null && hit.transform.root == player.root)
                    continue;

                // If the first object we can actually see is Pac-Man,
                // then he is visible.
                if (hit.collider == pacmanCollider)
                {
                    visible = true;
                }

                // Stop checking after the first non-player hit.
                break;
            }
        }

        // Play the sound only when Pac-Man first becomes visible.
        if (visible && !wasVisible)
        {
            audioSource.Play();
        }

        wasVisible = visible;
    }
}