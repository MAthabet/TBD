using UnityEngine;

public class BossLogic : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] private float offset;
    public float MaxHP = 90;
    public float currentHP;
    private bool canMoveDown = true;
    private bool canMoveUp = true;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHP = MaxHP;
    }

    void Update()
    {
        if (player == null) return;
        Vector3 newPosition = transform.position;
        float targetY = player.position.y + offset;

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
    public void TakeDamage(float dmaage)
    {
        currentHP -= dmaage;
        //UiManager.Instance.UpdateBossHealthBar();
    }
}
