using UnityEngine;

public class playerRotation : MonoBehaviour
{
    public Transform railwayCenter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = railwayCenter.position;
        temp.y = transform.position.y;
        transform.LookAt(temp);
    }
}
