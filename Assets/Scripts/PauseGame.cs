using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // For quitting or loading scenes

public class PauseGame : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign a Canvas with 2 buttons in Inspector
    private bool isPaused = false;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                Pause();
        }
    }

    void Pause()
    {
        Time.timeScale = 0f; // Freeze the game
        isPaused = true;
        pauseMenuUI.SetActive(true); // Show pause menu

        // Unlock and show cursor so player can click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume game
        isPaused = false;
        pauseMenuUI.SetActive(false);

        // Lock and hide cursor for normal gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Debug.Log("Game Resumed");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in editor
#else
        Application.Quit(); // Quit build
#endif
    }
}
