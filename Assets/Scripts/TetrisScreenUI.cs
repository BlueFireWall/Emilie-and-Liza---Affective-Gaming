using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // Load the Game Selection scene
        SceneManager.LoadScene("Main");
    }
}
