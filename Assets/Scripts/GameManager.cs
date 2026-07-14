using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;

    [Header("Scene Objects")]
    public Transform coinManager;
    public Transform exitWall;

    private int score = 0;
    private int totalCoins;
    private AudioSource coinAudio;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        coinAudio = GetComponent<AudioSource>();

        totalCoins = coinManager.childCount;
        UpdateScore();
    }

    public void CollectCoin()
    {
        score++;

        if (coinAudio != null)
            coinAudio.Play();

        UpdateScore();

        if (score >= totalCoins)
        {
            WinGame();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");
        scoreText.text = "Congrats, you win!";
        exitWall.position += Vector3.up * 100f;
    }
}