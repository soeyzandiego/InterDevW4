using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float smoothTime;

    [Header("Bounds")]
    [SerializeField] float leftBound;
    [SerializeField] float rightBound;

    Vector3 vel = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.position + offset;
        targetPos.z = -10;
        targetPos.x = Mathf.Clamp(targetPos.x, leftBound, rightBound);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime);
    }
}
