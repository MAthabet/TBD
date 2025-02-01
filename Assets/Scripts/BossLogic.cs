using UnityEngine;

public class BossLogic : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    private bool canMoveDown = true;
    private bool canMoveUp = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        float targetY = player.position.y - 19;

        if (targetY > transform.position.y && canMoveUp)
        {
            newPosition.y = Mathf.Min(targetY, maxHeight);
            if (newPosition.y >= maxHeight)
            {
                canMoveUp = false;
            }
        }
        else if (targetY < transform.position.y && canMoveDown) 
        {
            newPosition.y = Mathf.Max(targetY, minHeight);
            if (newPosition.y <= minHeight)
            {
                canMoveDown = false;
            }
        }

        transform.position = newPosition;

        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle-180, 0);
    }
}
