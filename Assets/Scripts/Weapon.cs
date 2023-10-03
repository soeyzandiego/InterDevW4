using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public float knockbackForce;
    [SerializeField] public float bulletSpeed = 10f;
    [SerializeField] public float bulletDelay = 0.2f;
    [SerializeField] public GameObject bulletPrefab;
}
