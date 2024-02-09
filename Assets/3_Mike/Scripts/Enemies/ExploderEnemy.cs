using UnityEngine;

public class ExploderEnemy : MeleeEnemy
{
    [SerializeField] private float explosionRadius = 3f;

    // Constructor modified to pass bulletSpawnPoints to the base class (MeleeEnemy)
    public ExploderEnemy(float _attackRange, float _attackTime, Transform[] _bulletSpawnPoints)
        : base(_attackRange, _attackTime, _bulletSpawnPoints)
    {}

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack(float interval)
    {
        // Custom attack logic for an exploding enemy, trigger the explosion when attacking
        Explode();
        base.Attack(interval);
    }

    private void Explode()
    {
        // Detect nearby players within the explosion radius and deal damage
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                IDamageable damageable = col.GetComponent<IDamageable>();
                if (damageable != null)
                    damageable.GetDamage(weaponDamage);
            }
        }
        //Play Explosion Sound
        SoundManager.soundManager.PlaySound(attackClip, transform, 0.35f);

        // Destroy the exploder enemy after exploding
        Destroy(gameObject); 
    }
}
