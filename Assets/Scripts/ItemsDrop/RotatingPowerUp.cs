using UnityEngine;

public class RotatingPowerUp : MonoBehaviour
{

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] GameObject powerUp;
    [Header("Hover Settings")]
    [SerializeField] private float hoverHeight = 0.5f; 
    [SerializeField] private float hoverSpeed = 2f; 
    private Vector3 startPosition; 

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        powerUp.transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}