using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackTime;
    [SerializeField] protected float weaponDamage = 1;
    [SerializeField] protected float bulletSpeed = 0;
    [SerializeField] protected Transform[] bulletSpawnPoints;

    [SerializeField] protected AudioClip attackClip;


    protected float timer = 0;
    private float setSpeed = 0;

    // Constructor modified to accept bulletSpawnPoints parameter
    public MeleeEnemy(float _attackRange, float _attackTime, Transform[] _bulletSpawnPoints)
    {
        attackRange = _attackRange;
        attackTime = _attackTime;
        bulletSpawnPoints = _bulletSpawnPoints;
    }

    /*public void SetMeleeEnemy(float _attackRange, float _attackTime)
    {
        attackRange = _attackRange;
        attackTime = _attackTime;
    }*/

    // Start is called before the first frame update


    protected override void Start()
    {
        base.Start();
        health = new Health(1, 0, 1);
        setSpeed = speed;

        Weapon enemyWeapon = new Weapon("Melee", weaponDamage, bulletSpeed, bulletSpawnPoints);
        weapon = enemyWeapon;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (target == null)
            return;

        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            //Enemy can Attack
            speed = 0;
            Attack(attackTime);
        }
        else
        {
            speed = setSpeed;
        }
    }

    public override void Attack(float interval)
    {
        if (timer <= interval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            target.GetComponent<IDamageable>().GetDamage(weapon.GetDamage());
            //Play Melee Attack Sound
            SoundManager.soundManager.PlaySound(attackClip, transform, 0.35f);
        }
    }
}
