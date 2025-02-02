using UnityEngine;

public class Scene1Game : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] float zOffset = -2;

    [SerializeField] float CamMoveSpeed;
    float z = 0;
    public bool cameraIsMoving = false;

    Transform target0;
    float playerPosLastFrame;
    float playerPosThisFrame;
    [SerializeField] Transform target1;
    void Start()
    {
        target0 = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cameraIsMoving);
        if (cameraIsMoving)
            transform.position = Vector3.Lerp(transform.position, target1.position, CamMoveSpeed * Time.deltaTime);
        if (player.position.z < 10)
        {
            cameraIsMoving = false;
            playerPosThisFrame = player.position.z;
            if(playerPosLastFrame == 0)
            {
                playerPosLastFrame = playerPosThisFrame;
            }
            z = playerPosThisFrame - playerPosLastFrame;
            if(z < 0)
                z += zOffset * Time.deltaTime;
            else if(z > 0)
            {
                z -= zOffset * Time.deltaTime;
            }
            transform.position = new Vector3( transform.position.x, transform.position.y , 
                transform.position.z + z);
            playerPosLastFrame = player.position.z;
        }
        else if(!cameraIsMoving)
        {
            z = 0;
            cameraIsMoving = true;
        }
    }
}
