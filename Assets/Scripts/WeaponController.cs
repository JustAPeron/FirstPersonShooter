using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region Variables
    
    public Transform barrelEnd;

    [Header("Ammo")]
    public int currentAmmo;
    public int maxAmmo;
    public bool infiniteAmmo;

    [Header("Statistics")]
    public float bulletSpeed;
    public float fireRate;
    public float damage;
    public float reloadSpeed;

    private ObjectPool pool;
    private float timeFromLastShot;

    private bool isPlayer;

    #endregion
    private void Awake()
    {
        //Checking if its a player or enemy using the weapon
        if (GetComponent<PlayerController>())
        {
            isPlayer = true;
        }
        pool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    /// <summary>
    /// checks if it can shoot
    /// </summary>
    /// <returns>bool</returns>
    public bool CanShoot()
    {
        // 1. fireRate
        if (Time.time - timeFromLastShot >= fireRate)
            if (currentAmmo > 0 || infiniteAmmo)
                return true;

        return false;
    }

    /// <summary>
    /// Handle Weapon Shooting
    /// </summary>
    public void Shoot()
    {
        // set time from last shot to 0
        timeFromLastShot = Time.time;

        // reduce Ammo if Ammo isnt infinite
        if (!infiniteAmmo) currentAmmo--;

        // get a bullet from the pool
        GameObject bullet = pool.GetGameObject();

        BulletController bulletCtrl = bullet.GetComponent<BulletController>();

        // position the bullet on the end of the barrel
        bullet.transform.position = barrelEnd.position;
        bullet.transform.rotation = barrelEnd.rotation;

        // assign variables to the bullet
        bullet.GetComponent<Rigidbody>().velocity = barrelEnd.forward * bulletSpeed;
        bulletCtrl.Damage = damage;
    }
}
