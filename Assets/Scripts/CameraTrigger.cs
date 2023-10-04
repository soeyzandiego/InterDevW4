using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] Transform nextPoint;

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            GetComponent<Collider2D>().isTrigger = false;
            FindObjectOfType<CameraController>().SetTarget(nextPoint.position);
        }    
    }
}
