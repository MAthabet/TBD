using UnityEngine;

public class SpiralStaircaseCamera : MonoBehaviour
{
    public Transform player;
    public Transform staircaseCenter;
    public float distanceFromPlayer = 5f;
    public float smoothSpeed = 5f;
    public float midPointOffset = 1f;
    private void LateUpdate()
    {
        Vector3 midPoint = staircaseCenter.position;
        midPoint.y = player.position.y + midPointOffset;
        Vector3 playerDirection = (player.position - midPoint).normalized;
        Vector3 cameraPosition = player.position + playerDirection * distanceFromPlayer ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(midPoint);
    }
}