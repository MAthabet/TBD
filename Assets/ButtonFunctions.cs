using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]private GameObject maincam;
    [SerializeField] private GameObject player;
    public void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.SetActive(true);
        maincam.GetComponent<Scene1Game>().cameraIsMoving = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
