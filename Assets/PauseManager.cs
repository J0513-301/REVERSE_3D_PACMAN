using UnityEngine;

public class PauseManager : MonoBehaviour
{
    bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;

            Time.timeScale = paused ? 0f : 1f;

            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = paused;
        }
    }
}