using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
