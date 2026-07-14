using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinScript : MonoBehaviour
{
    private bool collected = false;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore if we've already collected this coin
        if (collected)
            return;

        // Accept any child object of the Player (camera, ground check, etc.)
        if (!other.transform.root.CompareTag("Player"))
            return;

        collected = true;

        // Increase the score
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CollectCoin();
        }

        // Hide the coin
        gameObject.SetActive(false);
    }
}