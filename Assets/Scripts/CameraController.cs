using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float smoothTime;

    Vector3 targetPos;
    Vector3 vel = Vector3.zero;

    void Start()
    {
        targetPos = transform.position;
    }

    void FixedUpdate()
    {
        targetPos.z = -10;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime);
    }

    public void SetTarget(Vector3 newTargetPos)
    {
        newTargetPos.y = transform.position.y;
        targetPos = newTargetPos;
    }
}
