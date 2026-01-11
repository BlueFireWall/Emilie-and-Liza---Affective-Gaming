using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScreenUI : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // Load the Game Selection scene
        SceneManager.LoadScene("Credit");
    }
}
