using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_Text difficultyText;
    public TMP_Text musicText;

    private int difficulty = 1;
    private bool musicOn = true;

    void Start()
    {
        // Default to Normal difficulty the first time the menu opens
        PlayerPrefs.SetFloat("PacmanSpeed", 5f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ChangeDifficulty()
    {
        difficulty++;

        if (difficulty > 3)
            difficulty = 0;

        switch (difficulty)
        {
            case 0:
                difficultyText.text = "Difficulty: Easy";
                PlayerPrefs.SetFloat("PacmanSpeed", 2f);
                break;

            case 1:
                difficultyText.text = "Difficulty: Normal";
                PlayerPrefs.SetFloat("PacmanSpeed", 5f);
                break;

            case 2:
                difficultyText.text = "Difficulty: Hard";
                PlayerPrefs.SetFloat("PacmanSpeed", 7f);
                break;

            case 3:
                difficultyText.text = "Difficulty: Nightmare";
                PlayerPrefs.SetFloat("PacmanSpeed", 15f);
                break;
        }

        PlayerPrefs.Save();
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;

        AudioListener.volume = musicOn ? 1f : 0f;

        musicText.text = musicOn ? "Music: ON" : "Music: OFF";
    }
}