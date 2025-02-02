using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField]private GameObject maincam;
    [SerializeField] private GameObject player;
    Transform target0;
    [SerializeField] Transform target1;
    [SerializeField] float CamMoveSpeed;
    bool CameraIsMoving = false;
    private void Start()
    { 
        target0 = maincam.transform;
    }
    public void Update()
    {
        if(CameraIsMoving && maincam.transform.position != target1.position)
        maincam.transform.position = Vector3.Lerp(maincam.transform.position, target1.position, CamMoveSpeed * Time.deltaTime);
    }
    public void MoveCamera()
    {
        CameraIsMoving = true;
    }
    public void PlayGame()
    {
        player.SetActive(true);
        MoveCamera();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
