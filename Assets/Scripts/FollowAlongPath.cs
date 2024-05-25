using UnityEngine;

/// <summary>
/// 
/// UNFINISHED SCRIPT, DON'T USE
/// 
/// </summary>
public class FollowAlongPath : MonoBehaviour
{
    public Transform toFollow;
    public Vector3 offset;
    public float t;
    public bool fixedUpdate;

    public LineRenderer camPath;

    private Vector3[] points;
    private int closestPoint;

    void Start()
    {
        if (camPath == null)
            camPath = FindObjectOfType<LineRenderer>();


        // get path points
        Vector3[] points = new Vector3[camPath.positionCount];
        for (int i = 0; i < points.Length; i++)
            points[i] = camPath.GetPosition(i);

        // get closest point
        int closestPointIndex = 0;
        float closestDistanceSqrd = (toFollow.position - points[closestPointIndex]).sqrMagnitude;
        for (int i = 1; i < points.Length; i++)
        {
            float distanceSqrd = (toFollow.position - points[i]).sqrMagnitude;
            if (distanceSqrd < closestDistanceSqrd)
            {
                closestPoint = i;
                closestDistanceSqrd = distanceSqrd;
            }
        }
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
        int otherPoint = GetOtherPoint();

        // switch closest point
        if (Vector3.Distance(toFollow.position, points[otherPoint]) < Vector3.Distance(toFollow.position, points[closestPoint]))
        {
            closestPoint = otherPoint;
            otherPoint = GetOtherPoint();
        }

        // DO STUFF HERE
    }

    private int GetOtherPoint()
    {
        if (closestPoint == 0)
            return 1;
        if (closestPoint == points.Length - 1)
            return closestPoint - 1;

        // DO STUFF HERE

        return 0;
    }
}
