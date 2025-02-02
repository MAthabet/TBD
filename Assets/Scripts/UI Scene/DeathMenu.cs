using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        StartCoroutine(WaitingMenu());
    }
    IEnumerator WaitingMenu()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
