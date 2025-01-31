using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using static Unity.Burst.Intrinsics.Arm;

public class SplineMoving : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float walkingSpeed = 1f;
    [SerializeField] private float accelrate = 3f;
    [SerializeField] private float acceleration = 0.3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float lookAheadDistance = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform rampCenter;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackAngle = 60f;
    public float playerAngelToCenter = 40;
    float lookingAngle = 40;

    private float currentPos;
    float splineLength;
    float currentSpeed;
    float targetSpeed;
    bool isGrounded = true;
    bool isAccelerating = false;
    bool isAttacking = false;
    CharacterController controller;
    private Vector3 velocity;
    private float lastYPos;
    private Animator animator;
    private int currentAttack = 1;
    float animatorVelocity = 0.1f;
    float decelration = 0;

    void Start()
    {
        splineLength = spline.Spline.GetLength();
        controller = GetComponent<CharacterController>();
        lastYPos = transform.position.y;
        animator = GetComponent<Animator>();
        targetSpeed = 0;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            UpdatePosOnSpline();
            HandleGravity();
            HandleAcceleration();
        }
    }

    private void HandleAcceleration()
    {
        if (currentSpeed == 0) return;
        if (isAccelerating && isGrounded)
        {
            targetSpeed = currentSpeed > 0 ? maxSpeed : -maxSpeed;
        }
        else
        {
            targetSpeed = currentSpeed > 0 ? walkingSpeed : -walkingSpeed;
        }
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, accelrate * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            
            if (controller.isGrounded)
            {
                velocity.y = 0;
                isGrounded = true;
            }
        }
    }

    private void UpdatePosOnSpline()
    {
        float nextPos = Mathf.Clamp(currentPos + currentSpeed * Time.deltaTime, 0f, splineLength);

        if(currentPos == nextPos)
        {
            animator.SetFloat("Velocity",0);
            return;
        }

        currentPos = nextPos;
        float normalizedPos = currentPos / splineLength;
        spline.Spline.Evaluate(normalizedPos, out var pos, out var tangent, out var _);
        Vector3 targetPos = spline.transform.TransformPoint(pos);
        if (!isGrounded)
        {
            targetPos.y = transform.position.y;
        }

        Vector3 movement = targetPos - transform.position;
        controller.Move(movement);
        Vector3 rampCenterToPlayer = rampCenter.position;
        rampCenterToPlayer.y = transform.position.y;
        Vector3 directionToCenter = (rampCenterToPlayer - transform.position).normalized;
        Vector3 lookDirection = Quaternion.Euler(0f, lookingAngle, 0f) * directionToCenter;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection);
        if(animatorVelocity < 1.0f)
            animatorVelocity += Time.deltaTime * acceleration;
        animator.SetFloat("Velocity", animatorVelocity);
    }

    void OnMove(InputValue val)
    {        
        if(val.Get<float>() < 0)
        {
            lookingAngle = playerAngelToCenter;
        }
        else if (val.Get<float>() > 0)
        {
            lookingAngle = playerAngelToCenter - 90;
        }
        currentSpeed = -val.Get<float>() * walkingSpeed;
        animatorVelocity = 0.1f;
    }

    void OnJump()
    {
        if (isAttacking) return;
        
        if (isGrounded)
        {
            velocity.y = jumpForce;
            animator.SetTrigger("jump");
            isGrounded = false;
        }
    }

    void OnAccelerate(InputValue val)
    {
        isAccelerating = val.isPressed;
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
        yield return new WaitForSeconds(2f); // Adjust this time based on your animation length

        isAttacking = false;
        animator.SetInteger("Attack", 0);
    }

}