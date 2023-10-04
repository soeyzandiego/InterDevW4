using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Positioning")]
    [SerializeField] Transform pivotPoint;
    [SerializeField] Transform shootPoint;

    [Space(40)]
    [SerializeField] SpriteRenderer weaponSprite;
    [SerializeField] List<Weapon> weapons = new List<Weapon>();
    [SerializeField] TMP_Text ammoText;

    PlayerMovement movementControl;

    int weaponIndex = 0;
    Weapon curWeapon;
    int magazine;

    float bulletTimer;

    // Start is called before the first frame update
    void Start()
    {
        movementControl = GetComponent<PlayerMovement>();
        curWeapon = weapons[weaponIndex];
        magazine = curWeapon.magazineSize;

        bulletTimer = curWeapon.bulletDelay;
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        SwapWeapon(Mathf.FloorToInt(Input.mouseScrollDelta.y));

        ammoText.text = magazine.ToString();
        if (Input.GetMouseButton(0) && magazine > 0)
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
        magazine--;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // TODO this is setup wrong right now, the velocity is still slower if shooting close to the body
        Vector3 dir = (mousePos - pivotPoint.position).normalized;
        dir.z = 0;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(curWeapon.bulletPrefab, shootPoint.position, Quaternion.Euler(0, 0, angle));
        bullet.GetComponent<Rigidbody2D>().velocity = dir * curWeapon.bulletSpeed;
        
        movementControl.Knockback(dir, curWeapon.knockbackForce);
    }

    void SwapWeapon(int dir)
    {
        if (dir == 1)
        {
            if (weaponIndex < weapons.Count - 1) { weaponIndex++; }
            else { weaponIndex = 0; }
        }
        else if (dir == -1)
        {
            if (weaponIndex > 0) { weaponIndex--; }
            else { weaponIndex = weapons.Count - 1; }
        }
        else
        {
            return;
        }

        curWeapon = weapons[weaponIndex];

        weaponSprite.sprite = curWeapon.sprite;
        bulletTimer = curWeapon.bulletDelay;
        magazine = curWeapon.magazineSize;
    }

    public void Reload()
    {
        magazine = curWeapon.magazineSize;
    }
}
