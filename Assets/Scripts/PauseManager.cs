using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public TMP_Text musicText;

    [SerializeField] private GameObject pauseCanvas;

    private bool paused = false;
    private bool musicOn = true;

    void Start()
    {

        // Make sure the pause menu starts hidden
        pauseCanvas.SetActive(false);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;

            // Pause or resume the game
            Time.timeScale = paused ? 0f : 1f;

            // Show or hide the pause menu
            pauseCanvas.SetActive(paused);

            // Unlock/lock the mouse
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = paused;
        }
    }
    public void ToggleMusic()
    {
        musicOn = !musicOn;

        AudioListener.volume = musicOn ? 1f : 0f;

        musicText.text = musicOn ? "Music: ON" : "Music: OFF";
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        PlayerPrefs.SetInt("Deaths", 0);
        PlayerPrefs.Save();

        // Resume time
        Time.timeScale = 1f;

        // Reset pause state
        paused = false;

        // Reset cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");

        Debug.Log("Deaths after reset = " + PlayerPrefs.GetInt("Deaths", -1));
    }
}