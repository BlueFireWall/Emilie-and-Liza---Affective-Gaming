using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [Tooltip("Scene to load if no previous scene is stored")]
    public string defaultScene = "TitleScreen";

    private void Awake()
    {
        // Store the current scene as "previous" for the next scene
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("PreviousScene", currentScene);
        PlayerPrefs.Save();
    }

    // Call this method from any Button's OnClick
    public void GoBack()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            string previousScene = PlayerPrefs.GetString("PreviousScene");

            // Avoid reloading the same scene
            if (previousScene != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(previousScene);
                return;
            }
        }

        // If no previous scene or same scene, load default
        SceneManager.LoadScene(defaultScene);
    }
}
