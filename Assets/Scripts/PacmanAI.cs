using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class PacmanAI : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Proximity Audio")]
    public AudioSource proximityAudio;
    public float maxAudioDistance = 20f;
    public float minAudioDistance = 3f;
    public float maxAudioVolume = 0.3f;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Use the difficulty selected in the main menu
        moveSpeed = PlayerPrefs.GetFloat("PacmanSpeed", 5f);
        agent.speed = moveSpeed;

        if (proximityAudio != null)
        {
            proximityAudio.loop = true;
            proximityAudio.playOnAwake = false;
            proximityAudio.volume = 0f;
        }
    }

    void Update()
    {
        // Keep the NavMeshAgent using the selected speed
        agent.speed = moveSpeed;

        // Chase the player
        if (player != null)
        {
            agent.SetDestination(player.position);
        }

        // Update proximity audio
        UpdateProximityAudio();

        // Check if Pac-Man caught the player
        if (player != null &&
            Vector3.Distance(transform.position, player.position) < 0.75f)
        {
            if (GameManager.Instance != null)
                GameManager.Instance.PlayerDied();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateProximityAudio()
    {
        if (player == null || proximityAudio == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= maxAudioDistance)
        {
            if (!proximityAudio.isPlaying)
                proximityAudio.Play();

            float volume = 1f - Mathf.InverseLerp(minAudioDistance, maxAudioDistance, distance);
            proximityAudio.volume = volume * maxAudioVolume;
        }
        else
        {
            if (proximityAudio.isPlaying)
                proximityAudio.Stop();
        }
    }
}