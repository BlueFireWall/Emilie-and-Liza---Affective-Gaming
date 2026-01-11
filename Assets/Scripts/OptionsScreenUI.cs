using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // Load the Game Selection scene
        SceneManager.LoadScene("Options");
    }
}
