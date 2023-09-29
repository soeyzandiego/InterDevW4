using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] Transform pivotPoint;

    PlayerMovement movementControl;

    // Start is called before the first frame update
    void Start()
    {
        movementControl = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = (mousePos - pivotPoint.position).normalized;

            movementControl.Knockback(dir, 40f);
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - pivotPoint.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mousePos.x > transform.position.x) // If facing right, don't flip the gun
        {
            pivotPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (mousePos.x < transform.position.x)
        {
            pivotPoint.transform.rotation = Quaternion.Euler(180, 0, -angle);
        }
    }
}
