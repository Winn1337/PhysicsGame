using UnityEngine;

public class Car : MonoBehaviour
{
    private Rigidbody rb;
    private Wheel[] wheels;

    [Header("Controls")]
    public KeyCode[] left;
    public KeyCode[] right;

    private float leftInput;
    private float rightInput;

    [Header("Physics")]
    public float accelerationTorque;
    public float brakeTorque;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<Wheel>();
    }

    void Update()
    {
        foreach (var key in left)
            if (Input.GetKey(key))
                leftInput += Time.deltaTime;

        foreach (var key in right)
            if (Input.GetKey(key))
                rightInput += Time.deltaTime;
    }

    void FixedUpdate()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.onGround)
            {
                rb.AddForce(transform.right * accelerationTorque * rightInput);
                rb.AddForce(-transform.right * accelerationTorque * leftInput);
            }
        }

        leftInput = 0f;
        rightInput = 0f;
    }
}
