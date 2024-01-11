using UnityEngine;

public class MachineGunEnemy : MeleeEnemy
{
    [SerializeField] protected float accuracy = 10f; // The lower the value, the higher the accuracy
    [SerializeField] protected Bullet bulletPrefab;

    // Constructor modified to pass bulletSpawnPoints to the base class (MeleeEnemy)
    public MachineGunEnemy(float _attackRange, float _attackTime, Transform[] _bulletSpawnPoints)
        : base(_attackRange, _attackTime, _bulletSpawnPoints)
    {}


    protected override void Update()
    {
        base.Update();

        if (target != null) // Target check before attacking
            Attack(attackTime);
    }

    public override void Attack(float interval)
    {
        if (Vector2.Distance(transform.position, target.position) <= attackRange)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                Shoot();
                timer = 0f;
            }
        }
    }

    public override void Shoot()
    {
        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;

            // Add randomness to the shooting direction (simulating reduced accuracy)
            float randomAngle = Random.Range(-accuracy, accuracy);
            Quaternion rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + randomAngle);

            // Access bulletSpawnPoints from the base class (MeleeEnemy)
            foreach (Transform spawnPoint in bulletSpawnPoints)
            {
                Bullet bulletInstance = Instantiate(bulletPrefab, spawnPoint.position, rotation);
                bulletInstance.SetBullet(weaponDamage, "Player", bulletSpeed); // Setting bullet properties
            }
        }
    }
}
