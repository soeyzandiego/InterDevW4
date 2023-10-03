using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform pivotPoint;
    [SerializeField] Transform shootPoint;
    [SerializeField] SpriteRenderer weaponSprite;
    [SerializeField] List<Weapon> weapons = new List<Weapon>();

    PlayerMovement movementControl;

    int weaponIndex = 0;
    Weapon curWeapon;

    float bulletTimer;

    // Start is called before the first frame update
    void Start()
    {
        movementControl = GetComponent<PlayerMovement>();
        curWeapon = weapons[weaponIndex];

        bulletTimer = curWeapon.bulletDelay;
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

        if (Input.GetMouseButton(0))
        {
            if (bulletTimer > 0)
            {
                bulletTimer -= Time.deltaTime;
            }
            else
            {
                Shoot();
                bulletTimer = curWeapon.bulletDelay;
            }
        }
        else
        {
            bulletTimer = 0;
        }
    }

    void RotateGun()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mousePos - pivotPoint.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (mousePos.x > transform.position.x) // If facing right, don't flip the gun
        {
            pivotPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (mousePos.x < transform.position.x)
        {
            pivotPoint.transform.rotation = Quaternion.Euler(180, 0, -angle);
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = (mousePos - pivotPoint.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(curWeapon.bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
        bullet.GetComponent<Rigidbody2D>().velocity = dir * curWeapon.bulletSpeed;
        
        movementControl.Knockback(dir, curWeapon.knockbackForce);
    }

    void SwapWeapon(int dir)
    {
        if (dir == 1)
        {
            if (weaponIndex < weapons.Count) { weaponIndex++; }
            else { weaponIndex = 0; }
        }
        else
        {
            if (weaponIndex > 0) { weaponIndex--; }
            else { weaponIndex = weapons.Count - 1; }
        }

        curWeapon = weapons[weaponIndex];

        weaponSprite.sprite = curWeapon.sprite;
        bulletTimer = curWeapon.bulletDelay;
    }
}
