using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponManager playerWeapon = collision.GetComponent<WeaponManager>();
        if (playerWeapon != null)
        {
            playerWeapon.Reload();
            Destroy(gameObject);
        }
    }
}
