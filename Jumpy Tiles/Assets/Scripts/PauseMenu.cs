using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject controlsScreen;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject preGameScreen;

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void EnableControlsScreen()
    {
        controlsScreen.SetActive(true);
        preGameScreen.SetActive(false);
    }
    public void DisableControlsScreen()
    {
        controlsScreen.SetActive(false);
        preGameScreen.SetActive(true);
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
