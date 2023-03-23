using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField]
    Transform firePoint;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float bulletSpeed;
    float shootCooldown = 0.5f;
    float lastShot;
    bool dead = false;


    private void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !dead)
        {
            if (Time.time - lastShot > shootCooldown)
            {
                Shoot();
                lastShot = Time.time;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 90));
        bullet.GetComponent<Rigidbody2D>().AddForce(-firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

    public void Death()
    {
        dead = true;
    }

}
