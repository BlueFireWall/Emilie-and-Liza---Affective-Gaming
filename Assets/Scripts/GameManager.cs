using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startButton, window, windowPause, pauseBtn;

    void Awake()
    {
        Time.timeScale = 0f;
    }

    public void OnClickStart()
    {
        Time.timeScale = 1f;
        window.SetActive(false);
        GetComponent<Movement>().StartGame();
    }

    // Load the TitleScreenUI scene instead of quitting
    public void OnClickExit()
    {
        SceneManager.LoadScene("GameSelection");
    }

    public void OnClickRestart()
    {
        windowPause.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickPause()
    {
        pauseBtn.GetComponent<Button>().interactable = false;
        Time.timeScale = 0f;
        windowPause.SetActive(true);
    }

    public void OnClickContinue()
    {
        pauseBtn.GetComponent<Button>().interactable = true;
        Time.timeScale = 1f;
        windowPause.SetActive(false);
    }
}
