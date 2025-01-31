using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Unity.VisualScripting;

public class PlayerController: MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float edgeCheckDistance = 0.2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 60f;
    [SerializeField] private float lookSenetivity = 5f;
    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deceleration = 12f;

    Vector2 moveInput;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isAttacking;
    private Animator animator;
    private int currentAttack = 1;
    private float lookVal;
    private Vector3 currentVelocity;
    private bool isSprinting;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        isGrounded = true;
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            HandleMovement();
            HandleGravity();
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        transform.Rotate(Vector3.up * lookVal * lookSenetivity * Time.deltaTime);
    }

    private void HandleGravity()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    Vector3 move;
    private void HandleMovement()
    {
        move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move = Vector3.ClampMagnitude(move, 1f);

        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        if (move.magnitude > 0)
        {
            Ray ray = new Ray(transform.position + Vector3.up, move.normalized);
            if (!Physics.Raycast(ray, edgeCheckDistance))
            {
                Ray downRay = new Ray(ray.GetPoint(edgeCheckDistance), Vector3.down);
                if (!Physics.Raycast(downRay, 3f))
                {
                    move = Vector3.zero;
                }
            }

            currentVelocity = Vector3.Lerp(currentVelocity, move * currentSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
            Debug.Log(currentVelocity);
        }

        animator.SetFloat("Velocity", currentVelocity.magnitude / sprintSpeed);
        controller.Move(currentVelocity * Time.deltaTime);
    }
    void OnAccelerate(InputValue value)
    {
        isSprinting = value.isPressed;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isAttacking || !isGrounded) return;
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        animator.SetTrigger("jump");
    }
    
    void OnLook(InputValue value)
    {
        lookVal = value.Get<Vector2>().x;
    }

    void OnAttack()
    {
        if (isAttacking) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float nearestDistance = float.MaxValue;
        Vector3 forward = transform.forward;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            directionToEnemy.y = 0;
            float angle = Vector3.Angle(forward, directionToEnemy);
            float distance = directionToEnemy.magnitude;

            if (distance <= attackRange && angle <= attackAngle / 2 && distance < nearestDistance)
            {
                nearestEnemy = enemy.transform;
                nearestDistance = distance;
            }
        }

        if (nearestEnemy != null)
        {
            Vector3 targetDirection = nearestEnemy.position - transform.position;
            targetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(targetDirection);
        }

        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetInteger("Attack", currentAttack);
        currentAttack = currentAttack == 1 ? 2 : 1;
        yield return new WaitForSeconds(2f);

        isAttacking = false;
        animator.SetInteger("Attack", 0);
    }
}