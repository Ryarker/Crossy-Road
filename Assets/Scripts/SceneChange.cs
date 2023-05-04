using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    private int replayButton = 1;
    private int menuButton = 0;

    public void Pause () {
        pauseButton.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume () {
        pauseButton.SetActive(false);
        Time.timeScale = 1f;
    }

    public void pauseToMenu () {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuButton);

    }
    public void replayGame () {
        
        SceneManager.LoadScene(replayButton);
        }
    
    public void BackMenu () {

        SceneManager.LoadScene(menuButton);
    }

    public void exitGame()
    {
        Application.Quit();
    }

}


