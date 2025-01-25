using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class SplineMoving : MonoBehaviour
{
    [SerializeField] private SplineContainer spline;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float lookAheadDistance = 0.1f;

    private float currentPos;
    float splineLength;
    float currentSpeed;

    void Start()
    {
        splineLength = spline.Spline.GetLength();
    }

    private void Update()
    {
        UpdatePosOnSpline();
    }

    private void UpdatePosOnSpline()
    {
        currentPos = Mathf.Clamp(currentPos + currentSpeed * Time.deltaTime, 0f, splineLength);
        
        float normalizedPos = currentPos / splineLength;
        spline.Spline.Evaluate(normalizedPos, out var pos, out var tangent, out var _);
        pos = spline.transform.TransformPoint(pos);
        tangent = spline.transform.TransformVector(tangent);
        Vector3 tang = tangent;
        Vector3 lookDirection = Quaternion.Euler(30f, 0f, 0f) * tang.normalized;
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void OnMove(InputValue val)
    {
        // i flipped the input and too lazy to edit it
        currentSpeed = -val.Get<float>() * maxSpeed;
    }
    void OnJump()
    {

    }
}