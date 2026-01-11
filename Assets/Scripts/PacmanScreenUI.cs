using UnityEngine;
using UnityEngine.SceneManagement;

public class PacmanScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // Load the Game Selection scene
        SceneManager.LoadScene("Pacman");
    }
}
