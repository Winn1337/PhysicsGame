using System;
using UnityEngine;

public class Car2D : MonoBehaviour
{
    [Header("Inputs")]
    public KeyCode[] left;
    public KeyCode[] right;

    private float leftInput;
    private float rightInput;

    [Header("Physics")]
    public Transform centerOfMass;
    public float wheelTorque;
    public float bodyTorque;
    public float maxSpeed;
    public AnimationCurve torqueCurve;
    public float forwardSpeed;
    public float normalizedSpeed;
    public float currentWheelTorque;
    public bool accelerate;

    [Header("References")]
    public Transform[] wheelModels;
    public Rigidbody2D rb;
    public Rigidbody2D[] wheelRb;

    private void Update()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {
        void InputAction(KeyCode[] keys, Action action)
        {
            foreach (var key in keys)
            {
                if (Input.GetKey(key))
                {
                    action.Invoke();
                    break;
                }
            }
        }

        InputAction(
            left, () =>
            leftInput += Time.deltaTime
            );

        InputAction(
            right, () =>
            rightInput += Time.deltaTime
            );
    }

    private void FixedUpdate()
    {
        //adjust center of mass so car doesn't fall over goofily
        if (rb.centerOfMass != (Vector2)centerOfMass.localPosition)
            rb.centerOfMass = centerOfMass.localPosition;

        //current speed forwards
        forwardSpeed = Vector3.Dot(transform.right, rb.velocity);

        //speed % of maxSpeed
        normalizedSpeed = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        //more speed => apply less torque
        currentWheelTorque = torqueCurve.Evaluate(normalizedSpeed) * wheelTorque;

        //input > 0 => go right
        //input < 0 => go left
        float input = rightInput - leftInput;

        //accelerate if input is same direction as speed
        accelerate = Mathf.Sign(input) == Mathf.Sign(forwardSpeed);

        //rb.AddForce(input * currentTorque * transform.right);

        foreach (var rb in wheelRb)
        {
            rb.AddTorque(-input * currentWheelTorque);
        }

        rb.AddTorque(input * bodyTorque);

        leftInput = 0f;
        rightInput = 0f;
    }
}
