using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform toFollow;
    public Vector3 offset;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = toFollow.position + offset;
    }
}
