using UnityEngine;
using UnityEngine.SceneManagement;

public class SodukoScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // Load the Game Selection scene
        SceneManager.LoadScene("Soduko");
    }
}
