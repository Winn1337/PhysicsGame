using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Car : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Inputs")]
    public KeyCode[] left;
    public KeyCode[] right;

    private float leftInput;
    private float rightInput;

    [Header("Physics")]
    public Transform centerOfMass;
    public float accelerationTorque;
    public float brakeTorque;
    public float maxSpeed;
    public AnimationCurve torqueCurve;
    public float forwardSpeed;
    public float normalizedSpeed;
    public float currentTorque;
    public bool accelerate;

    [Header("Models")]
    public Transform[] wheelModels;

    private WheelCollider[] wheels;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();
    }

    private void Update()
    {
        UpdateInputs();
        UpdateWheelModels();
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

    private void UpdateWheelModels()
    {
        for (int i = 0; i < wheelModels.Length; i++)
        {
            wheels[i].GetWorldPose(out Vector3 position, out Quaternion rotation);
            wheelModels[i].transform.position = position;
            wheelModels[i].transform.rotation = rotation;
        }
    }

    private void FixedUpdate()
    {
        //adjust center of mass so car doesn't fall over goofily
        if (rb.centerOfMass != centerOfMass.localPosition)
            rb.centerOfMass = centerOfMass.localPosition;

        //current speed forwards
        forwardSpeed = Vector3.Dot(transform.forward, rb.velocity);

        //speed % of maxSpeed
        normalizedSpeed = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        //more speed => apply less torque
        //currentTorque = Mathf.Lerp(accelerationTorque, 0, normalizedSpeed);
        currentTorque = torqueCurve.Evaluate(normalizedSpeed) * accelerationTorque;

        //input > 0 => go right
        //input < 0 => go left
        float input = rightInput - leftInput;

        //accelerate if input is same direction as speed
        accelerate = Mathf.Sign(input) == Mathf.Sign(forwardSpeed);

        foreach(var wheel in wheels)
        {
            wheel.motorTorque = 0f;
            wheel.brakeTorque = 0f;

            if (accelerate)
            {
                wheel.motorTorque = input * currentTorque;
            }
            else
            {
                wheel.brakeTorque = Mathf.Abs(input) * brakeTorque;
            }
        }

        leftInput = 0f;
        rightInput = 0f;
    }
}
