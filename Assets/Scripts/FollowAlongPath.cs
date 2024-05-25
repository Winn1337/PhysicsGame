using System.Drawing;
using UnityEngine;

public class FollowAlongPath : MonoBehaviour
{
    public Transform toFollow;
    public Vector3 offset;
    public float t;
    public bool fixedUpdate;

    public LineRenderer camPath;

    void Start()
    {
        if (camPath == null)
            camPath = FindObjectOfType<LineRenderer>();
    }

    private void Update()
    {
        if (!fixedUpdate)
            Follow(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (fixedUpdate)
            Follow(Time.fixedDeltaTime);
    }

    private void Follow(float dt)
    {
        // get path points
        Vector3[] points = new Vector3[camPath.positionCount];
        for (int i = 0; i < points.Length; i++)
            points[i] = camPath.GetPosition(i);

        int closestPointIndex = 0;
        float closestDistanceSqrd = (toFollow.position - points[closestPointIndex]).sqrMagnitude;
        for (int i = 1; i < points.Length; i++)
        {
            float distanceSqrd = (toFollow.position - points[i]).sqrMagnitude;
            if (distanceSqrd < closestDistanceSqrd)
                closestPointIndex = i;
        }

        Vector3 closestPoint = points[closestPointIndex];

        Debug.DrawRay(closestPoint, Vector3.up);
    }
}
