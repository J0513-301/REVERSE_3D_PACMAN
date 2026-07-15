using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TMP_Text scoreText;
    public TMP_Text deathText;

    [Header("Scene Objects")]
    public Transform coinManager;
    public Transform exitWall;

    private int score = 0;
    private int deaths;
    private int totalCoins;
    private AudioSource coinAudio;

    void Awake()
    {
        Instance = this;
    }

    void UpdateDeaths()
    {
        deathText.text = "Deaths: " + deaths;
    }

    void Start()
    {
        coinAudio = GetComponent<AudioSource>();

        totalCoins = coinManager.childCount;

        score = 0;

        deaths = PlayerPrefs.GetInt("Deaths", 0);

        UpdateScore();
        UpdateDeaths();

        Debug.Log("Deaths in PlayerPrefs = " + PlayerPrefs.GetInt("Deaths", -1));
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

    public void PlayerDied()
    {
        deaths++;

        PlayerPrefs.SetInt("Deaths", deaths);
        PlayerPrefs.Save();

        UpdateDeaths();
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