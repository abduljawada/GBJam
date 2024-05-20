using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] TextMeshProUGUI ammoText; 
    [SerializeField] bool canHold = false;
    [SerializeField] int damage = 1;
    [SerializeField] float bulletTime = 2f;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float shootingDelay = 1f;

    [SerializeField] int maxAmmo = 10;
    int currentAmmo;
    [SerializeField] float reloadTime = 1f;

    bool isReloading = false;

    float nextShootTime;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        ammoText.text = "Ammo" + Environment.NewLine + currentAmmo.ToString();
        transform.localEulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReloading)
        {
            if (Input.GetKeyDown(KeyCode.X) && currentAmmo != maxAmmo)
            {
                StartCoroutine(Reload());
                return;
            }
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (Time.time > nextShootTime)
                {
                    Shoot();
                    return;
                }
            }
            if (canHold)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    if (Time.time > nextShootTime)
                    {
                        Shoot();
                        return;
                    }
                }
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        transform.DOLocalRotate(new Vector3(0, 0, 180), reloadTime/2);
        yield return new WaitForSeconds(reloadTime/2);
        transform.DOLocalRotate(new Vector3(0, 0, 0), reloadTime / 2);
        yield return new WaitForSeconds(reloadTime / 2);
        currentAmmo = maxAmmo;
        isReloading = false;
        ammoText.text = "Ammo" + Environment.NewLine + currentAmmo.ToString();
    }

    void Shoot()
    {
        nextShootTime = Time.time + shootingDelay;

        currentAmmo--;
        ammoText.text = "Ammo" + Environment.NewLine + currentAmmo.ToString();


        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletDamage>().damage = damage;
        bullet.GetComponent<Timer>().timeLeft = bulletTime;
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
