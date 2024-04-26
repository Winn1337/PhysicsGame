using UnityEngine;

public class Bob : MonoBehaviour
{
    public Vector3 bounceDistance;
    public float bouncesPerSecond;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        transform.position = originalPosition + Mathf.Sin(Time.time * bouncesPerSecond) * bounceDistance;
    }
}
