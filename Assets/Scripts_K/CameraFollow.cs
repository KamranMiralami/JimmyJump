using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool isFollowing = true;
    private void Start()
    {
        if (isFollowing)
        {
            offset = transform.position - target.position;
        }
    }
    private void Update()
    {
        if (isFollowing)
        {
            transform.position = target.position + offset;
        }
    }
}
