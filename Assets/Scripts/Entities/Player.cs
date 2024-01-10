using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//this is instance CLASS (because we can create an instance of it), also known as non static class. Static class can not be istantiated, and cannot be used as an object
public class Player : PlayableObject
{
    //private string nickname;

    [Header("Player Settings")]
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Bullet burstBulletPrefab;
    [SerializeField] private GameObject gunPowerWeapon;


    [Header("Weapon Settings")]
    [SerializeField] private float weaponDamage = 1;
    [SerializeField] private float bulletSpeed = 10;
    [SerializeField] private float attackTime = 0.4f;
    [SerializeField] private int bulletsInBurts = 20;
    [SerializeField] private float burstInterval = 0.1f; // Time between each bullet in a burst
    [SerializeField] private float burstDuration = 3; // Time of GunPower pickup duaration
    
    public Action OnDeath;
    private Rigidbody2D playerRB;
    private  Bullet bullet;
    private bool isGunPowerFiring = false;
    private int burstSize; // Number of bullets to fire in a burst
    private float attackTimeReset;
    private float burstTimer = 0f; // Timer for controlling burst firing
    private float attackTimer = 0f;
    public float AttackTimer { get { return attackTimer; } set { attackTimer = value; } }
    private float gunPowerScale = 0.3f; // Initial GunPower scale


    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        bullet = bulletPrefab;

        gunPowerWeapon.SetActive(false);
        burstSize = bulletsInBurts;
        attackTimeReset = attackTime;

        weapon = new Weapon("Player weapon", weaponDamage, bulletSpeed);
        health = new Health(100, 0.5f, 100);
        gunPower = new GunPower(burstDuration, 0);
        nuke = new Nuke(0);

        cam = Camera.main; // Assigning main camera automaticly if it is removed from prefab
    }

    private void Update()
    {
        health.RegenHealth(); 

        attackTimer += Time.deltaTime;

        ShootGunPower();
        UpdateGunPower();            
    }

    // Trigger a nuke action
    public void Nuke()
    {
        Debug.Log($"Player using nuke");
        nuke.UseNuke();
    }

    // Check if gun power is active, initiate gun power firing or regular shooting based on timer
    public void UseWeapon()
    {
        if (gunPower.GetIsGunPower())
        {
            isGunPowerFiring = true;
            burstTimer = 0f;
        }
        else if (attackTimer >= attackTime)
        {            
            Shoot();
            attackTimer = 0f; // Reset the attack timer
        }
    }

    // Update gun power status and behavior
    private void UpdateGunPower()
    {
        if (gunPower.GetIsGunPower())
        {
            gunPower.DeductGunPower(Time.deltaTime);
            bulletPrefab = burstBulletPrefab;

            gunPowerWeapon.SetActive(true);
            
            // Visual scaling of GunPower load
            if (gunPower.GetCurrentGunPower() > burstDuration - 1)
                gunPowerScale = 0.3f; 
            else
                gunPowerScale = Mathf.Lerp(gunPowerScale, 0f, Time.deltaTime * gunPower.GetCurrentGunPower() * 0.1f);

            gunPowerWeapon.transform.Find("WeaponCharge1").localScale = Vector3.one * gunPowerScale;
            gunPowerWeapon.transform.Find("WeaponCharge2").localScale = Vector3.one * gunPowerScale;

            weapon = new Weapon("Player weapon", weaponDamage, 50);
            attackTime = 0.1f;
        }  
        else
        {
            gunPowerScale = 0.3f; // Visual reset of GunPower load
            
            gunPowerWeapon.SetActive(false);
            weapon = new Weapon("Player weapon", weaponDamage, bulletSpeed);    
            attackTime = attackTimeReset;
            bulletPrefab = bullet; 
        }
    }

    // Trigger gun power burst shots when available
    private void ShootGunPower()
    {
        if (isGunPowerFiring)
        {
            burstTimer += Time.deltaTime;

            // Loop to handle firing bullets within the burst interval
            while (burstTimer >= burstInterval)
            {
                burstTimer -= burstInterval;
                if (burstSize > 0)
                {
                    Shoot();
                    burstSize--;
                }
                else
                {
                    isGunPowerFiring = false;
                    burstSize = bulletsInBurts; // Reset burst size for next burst           
                    break;
                }
            }
        }
    }

    public override void Shoot()
    {
        if (attackTimer >= attackTime)
        {
            Debug.Log("Player shooting a bullet!");
            weapon.Shoot(bulletPrefab, this, "Enemy");

            // Reset the attack timer after shooting
            attackTimer = 0f;
        }
    }

    public override void Die()
    {
        Debug.Log($"Player died!");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }

    /*public override void Move(Vector2 direction, Vector2 target)
    {
        playerRB.velocity = direction * speed * Time.deltaTime;

        // to conver main camera to 2D view - convert from world view to screen view 
        var playerScreenPos = cam.WorldToScreenPoint(transform.position);
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }*/

    //  "Speed" parameter was chabged from 250 to 7
    public override void Move(Vector2 direction, Vector2 target)
    {
        Vector2 cameraBounds = GetCameraBounds();

        // Calculate the desired position based on the player's input and speed
        Vector3 newPosition = transform.position + new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime;

        // Restrict player's movement within camera view
        newPosition.x = Mathf.Clamp(newPosition.x, -cameraBounds.x, cameraBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, -cameraBounds.y, cameraBounds.y);

        // Apply the constrained position
        playerRB.MovePosition(newPosition);

        // Convert main camera to 2D view - convert from world view to screen view
        var playerScreenPos = cam.WorldToScreenPoint(transform.position);
        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Check camera bounds to prevent the player from moving beyond the borders of the main camera
    public Vector2 GetCameraBounds()
    {
        Vector2 cameraBounds = Vector2.zero;

        if (cam != null)
        {
            // Get the extents of the camera's viewport in world coordinates
            float camHeight = cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            // Calculate boundaries considering the camera's position
            cameraBounds.x = camWidth;
            cameraBounds.y = camHeight;
        }

        return cameraBounds;
    }

    public override void Attack(float interval) { }

    public override void GetDamage(float damage)
    {
        Debug.Log($"Player receiving damage");
        health.DeducHealth(damage);

        if (health.GetHealth() <= 0)
            Die();
    }
}
