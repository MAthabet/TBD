using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    AsyncOperation asyncOperation;
    [SerializeField] private TextMeshProUGUI text;
    void Start()
    {
        AudioManager.Instance.PlayMusic("MainGameMusic");
        asyncOperation = SceneManager.LoadSceneAsync("Second Scene", LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;
    }
    // Update is called once per frame
    
    void Update()
    {
        if(asyncOperation.progress >= 0.9f)
        {
            text.enabled = true;
            if (Input.GetKey(KeyCode.Escape))
            {
                AudioManager.Instance.StopMusic("MainGameMusic");
                asyncOperation.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync("Story Scene");

            }
        }
    }
}
