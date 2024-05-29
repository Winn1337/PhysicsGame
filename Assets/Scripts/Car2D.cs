using System;
using UnityEngine;

public class Car2D : MonoBehaviour
{
    [Header("Inputs")]
    public KeyCode left;
    public KeyCode right;
    public KeyCode handbrake;

    private float leftInput;
    private float rightInput;

    [Header("Physics")]
    public Transform centerOfMass;
    public float wheelTorque;
    public float bodyTorque;
    public float maxSpeed;
    public AnimationCurve torqueCurve;

    [Header("Debug")]
    public float forwardSpeed;
    public float normalizedSpeed;
    public float currentWheelTorque;

    [Header("Sound")]
    public AudioClip idle;
    public AudioClip drive;
    public float drivePitchChange;
    public float driveAudioSmoothSpeed;

    private SoundManager soundManager;
    private AudioSource driveAudioSource;

    [Header("References")]
    public Rigidbody2D rb;

    private WheelJoint2D[] wheelJoint;

    private void Start()
    {
        wheelJoint = GetComponentsInChildren<WheelJoint2D>();
        soundManager = SoundManager.Instance();

        soundManager.PlaySoundEffect(idle, transform, 0.25f, true);
        driveAudioSource = soundManager.PlaySoundEffect(drive, transform, 0f, true);
    }

    private void Update()
    {
        UpdateInputs();
        UpdateSound();
    }

    private void UpdateInputs()
    {
        //void InputAction(KeyCode[] keys, Action action)
        //{
        //    foreach (var key in keys)
        //    {
        //        if (Input.GetKey(key))
        //        {
        //            action.Invoke();
        //            break;
        //        }
        //    }
        //}

        //InputAction(
        //    left, () =>
        //    leftInput += Time.deltaTime
        //    );

        //InputAction(
        //    right, () =>
        //    rightInput += Time.deltaTime
        //    );

        if (Input.GetKey(left))
            leftInput += Time.deltaTime;
        if (Input.GetKey(right))
            rightInput += Time.deltaTime;
    }

    private float maxAngVel = 9000;

    private void UpdateSound()
    {
        float volume = Mathf.InverseLerp(0, maxAngVel, Mathf.Abs(wheelJoint[0].connectedBody.angularVelocity));
        if (Mathf.Abs(rightInput - leftInput) == 0f)
            volume = 0f;
        volume = Mathf.Lerp(driveAudioSource.volume, volume, driveAudioSmoothSpeed * Time.deltaTime);
        float pitch = 0.5f + volume * drivePitchChange;


        soundManager.SetVolume(driveAudioSource, volume);
        soundManager.SetPitch(driveAudioSource, pitch);
    }

    private void FixedUpdate()
    {
        //adjust center of mass so car doesn't fall over goofily
        if (rb.centerOfMass != (Vector2)centerOfMass.localPosition)
        rb.centerOfMass = (Vector2)centerOfMass.localPosition;

        //current speed forwards
        forwardSpeed = Vector3.Dot(transform.right, rb.velocity);

        //speed % of maxSpeed
        normalizedSpeed = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        //more speed => apply less torque
        currentWheelTorque = torqueCurve.Evaluate(normalizedSpeed) * wheelTorque;

        //input > 0 => go right
        //input < 0 => go left
        float input = rightInput - leftInput;

        //rb.AddForce(input * currentTorque * transform.right);
        //foreach (var rb in wheelRb)
        //    rb.AddTorque(-input * currentWheelTorque / wheelRb.Length);

        foreach (var joint in wheelJoint)
        {

            var motor = joint.motor;

            if (Input.GetKey(handbrake))
                motor.motorSpeed = 0f;
            else if (Mathf.Sign(input) == Mathf.Sign(motor.motorSpeed))
                motor.motorSpeed = 0f;
            else if (input == 0f)
                motor.motorSpeed = joint.connectedBody.angularVelocity;

            motor.motorSpeed -= input * currentWheelTorque / 0.5f * Time.fixedDeltaTime;

            joint.motor = motor;

            if (input == 0f && !Input.GetKey(handbrake))
                joint.useMotor = false;
            else
                joint.useMotor = true;
        }

        rb.AddTorque(input * bodyTorque);

        leftInput = 0f;
        rightInput = 0f;
    }
}
