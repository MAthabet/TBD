using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class SplineMoving : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float lookAheadDistance = 0.1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform rampCenter;
    public float playerAngelToCenter = 40;
    float lookingAngle = 40;

    private float currentPos;
    float splineLength;
    float currentSpeed;
    bool isGrounded = true;
    public bool isAttacking = false;
    CharacterController controller;
    private Vector3 velocity;
    private float lastYPos;

    void Start()
    {
        splineLength = spline.Spline.GetLength();
        controller = GetComponent<CharacterController>();
        lastYPos = transform.position.y;
    }

    private void Update()
    {
        UpdatePosOnSpline();
        HandleGravity();
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
        currentPos = Mathf.Clamp(currentPos + currentSpeed * Time.deltaTime, 0f, splineLength);
        
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
        currentSpeed = -val.Get<float>() * maxSpeed;
    }

    void OnJump()
    {
        Debug.Log("Jump");
        if (isGrounded)
        {
            Debug.Log("Jumping");
            velocity.y = jumpForce;
            isGrounded = false;
        }
    }

    void OnAttack()
    {
        if (!isAttacking)
            attack();
    }
    void attack()
    {
        isAttacking = true;

    }
}