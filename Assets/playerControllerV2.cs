using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class PlayerControllerV2 : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;


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
            isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.3f);
            if (isGrounded)
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

    void OnAttack()
    {
        if (isAttacking) return;
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetInteger("Attack", currentAttack);
        currentAttack = currentAttack == 1 ? 2 : 1;
        //AudioManager.Instance.PlaySFX("PlayerAttack");
        yield return new WaitForSeconds(2f);

        isAttacking = false;
        animator.SetInteger("Attack", 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.StopAllMusic();
        AudioManager.Instance.StopAllSFX();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}