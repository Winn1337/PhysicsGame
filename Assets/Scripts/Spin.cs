using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 degreesPerSecond;

    private void Update()
    {
        transform.Rotate(degreesPerSecond * Time.deltaTime);
    }
}
