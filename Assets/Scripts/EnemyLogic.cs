using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private bool canJump;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float yDetectionThreshold = 2f;

    private Transform player;
    private CharacterController controller;
    private bool isAttacking;
    private bool hasJumped;
    private float verticalVelocity;
    private float gravity = -9.81f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (player == null) return;

        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
            hasJumped = false;
        }

        verticalVelocity += gravity * Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float yDifference = Mathf.Abs(transform.position.y - player.position.y);

        if (yDifference <= yDetectionThreshold && distanceToPlayer <= detectionRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Vector3 movement = directionToPlayer * moveSpeed;
            
            if (canJump && !hasJumped && distanceToPlayer <= attackRange * 1.5f)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                hasJumped = true;
            }

            movement.y = verticalVelocity;
            controller.Move(movement * Time.deltaTime);

            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
            isAttacking = false;
        }
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            // Attack logic here
        }
    }
}
