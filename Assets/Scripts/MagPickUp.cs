using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPickUp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        WeaponManager playerWeapon = collision.gameObject.GetComponent<WeaponManager>();
        if (playerWeapon != null)
        {
            playerWeapon.Reload();
            Destroy(gameObject);
        }
    }

}
