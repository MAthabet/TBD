using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private bool canJump;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float yDetectionThreshold = 2f;
    public float Damage;

    private Transform player;
    private CharacterController controller;
    private bool isAttacking;
    private bool hasJumped;
    private float verticalVelocity;
    private float gravity = -9.81f;
    Animator animator;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
            
            // Make enemy look at player
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            
            if (canJump && !hasJumped && distanceToPlayer <= attackRange * 1.5f)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                hasJumped = true;
            }

            movement.y = verticalVelocity;
            controller.Move(movement * Time.deltaTime);
            animator.SetBool("isRunning", true);
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
            isAttacking = false;
            animator.SetBool("isRunning", false);
        }
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            animator.SetBool("isRunning", false);
            animator.SetTrigger("Attack");
            isAttacking = true;
            // Attack logic here
        }
    }
    private void gitHit()
    {
        //gitHit logic
        animator.SetTrigger("Hit");
    }
    private void Die()
    {
        //Die logic
        animator.SetTrigger("Death");
    }
}
