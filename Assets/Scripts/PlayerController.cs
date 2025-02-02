using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 60f;

    [SerializeField] private float acceleration = 8f;
    [SerializeField] private float deceleration = 12f;

    [SerializeField] private float rotationSpeed = 720f;

    Vector2 moveInput;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isAttacking;
    private Animator animator;
    private int currentAttack = 1;
    private Vector3 currentVelocity;
    private bool isSprinting;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isGrounded = true;
        currentSpeed = 0;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);
            if(isGrounded)
            {
                animator.SetBool("isGrounded", true);
            }
            else
                animator.SetBool("isGrounded", false);
            HandleMovement();
            HandleGravity();
        }
    }

    private void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    private void HandleMovement()
    {
        // Convert input to camera relative direction
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;
        Vector3 inputRaw = right * moveInput.x + forward * moveInput.y;
        
        if (inputRaw != Vector3.zero)
        {
            float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
            currentSpeed = targetSpeed;
            currentVelocity = inputRaw.normalized * currentSpeed;

            // Immediately rotate to face movement direction
            transform.rotation = Quaternion.LookRotation(inputRaw);
        }
        else
        {
            currentSpeed = 0;
            currentVelocity = Vector3.zero;
            animator.SetFloat("Velocity", Mathf.Abs(currentSpeed) / sprintSpeed);
            return;
        }
        controller.Move(currentVelocity * Time.deltaTime);
        animator.SetFloat("Velocity", Mathf.Abs(currentSpeed) / sprintSpeed);
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
        animator.SetBool("isGrounded", false);
    }
    void OnMagicAttack()
    {
            PlayerStats p = GetComponent<PlayerStats>();
        if ((p.currentMagicCharge/p.maxMagicCharge)*100 >= 50)
        {
            animator.SetTrigger("MagicAttack");
            p.currentMagicCharge -= p.currentMagicCharge * 0.3f;
            UiManager.Instance.UpdateMagicCharge();
        }
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
        if(AudioManager.Instance != null)
        AudioManager.Instance.PlaySFX("PlayerAttack");
        yield return new WaitForSeconds(2f);

        isAttacking = false;
        animator.SetInteger("Attack", 0);
    }
}