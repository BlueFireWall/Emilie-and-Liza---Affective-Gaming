using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButtonManager : MonoBehaviour
{
    public static void SaveCurrentScene()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    }

    public void GoBack()
    {
        string lastScene = PlayerPrefs.GetString("LastScene", "TitleScreen");
        SceneManager.LoadScene(lastScene);
    }

    public void GoToTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
