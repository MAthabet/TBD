using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    public void Home()
    {
        SceneManager.LoadSceneAsync("MainScene");
        Time.timeScale = 1;
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
