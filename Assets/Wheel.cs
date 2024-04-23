using UnityEngine;

public class Wheel : MonoBehaviour
{
    private Rigidbody rb;
    private Car car;

    [Header("Suspension")]
    public float restLength;
    public float springTravel;
    public float springStiffness;
    public float damperStiffness;

    private float minLength;
    private float maxLength;
    private float lastLength;
    private float springLength;
    private float springVelocity;
    private float springForce;
    private float damperForce;

    private Vector3 suspensionForce;

    [Header("Wheel")]
    public float wheelRadius;
    public Transform wheel;

    public bool onGround;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();
        car = transform.root.GetComponent<Car>();
    }

    void FixedUpdate()
    {
        minLength = restLength - springTravel;
        maxLength = restLength + springTravel;

        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength + wheelRadius))
        {
            lastLength = springLength;
            springLength = Mathf.Clamp(hit.distance - wheelRadius, minLength, maxLength);

            springVelocity = (lastLength - springLength) / Time.fixedDeltaTime;

            springForce = springStiffness * (restLength - springLength);
            damperForce = damperStiffness * springVelocity;

            suspensionForce = (springForce + damperForce) * transform.up;


            rb.AddForceAtPosition(suspensionForce, hit.point);

            //purely cosmetic
            wheel.position = hit.point + transform.up * wheelRadius;

            onGround = true;
        }
        else
        {
            //wheel.position = transform.position - transform.up * maxLength;

            onGround = false;
        }
    }
}
