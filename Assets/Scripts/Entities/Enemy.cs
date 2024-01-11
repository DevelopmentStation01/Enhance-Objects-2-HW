using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : PlayableObject
{
    //private string name;
    [SerializeField] protected float speed;
    [SerializeField] private  GameObject deathEffect;

    protected Transform target;

    private EnemyType enemyType;

    private enum TestEnum
    {
        value1, value2, value3
    }


    // if enum public - should be public and if it is private should be private. The access has to match if enum is inserted in another class
    private TestEnum testEnum; 

    // to fllow player's game object
    protected virtual void Start()
    {
        deathEffect.SetActive(false);

        try 
        {
            target = GameObject.FindWithTag("Player").transform;
        }
        catch (NullReferenceException e)
        {
            Debug.Log($"Ther's no enemy target in the scene, destroying myself {e}");
            GameManager.GetInstance().isEnemySpawning = false;
        }
    }

    protected virtual void Update()
    {
        //Setting target
        if (target != null)
        {
            Move(target.position);
        }
        else
        {
            Move(speed);
        }
    }

    public override void Move(Vector2 direction, Vector2 target) { }

    /// <summary>
    /// Using the move method to move the nemey automatically without the player/target
    /// </summary>
    /// <param name="speed"></param>
    public override void Move(float speed)
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public override void Move(Vector2 direction)
    {
        direction.x -= transform.position.x;
        direction.y -= transform.position.y;

        float angle  = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        transform.Translate(Vector2.right * speed * Time.deltaTime );
    }

    public override void Shoot() { }

    public override void Attack(float interval) { }

    public override void Die()
    {
        StartCoroutine(RunDeathEffect());
    }

    IEnumerator RunDeathEffect()
    {
        deathEffect.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        Debug.Log($"Enemy died");
        GameManager.GetInstance().NotifyDeath(this);
        
        Destroy(gameObject);
    }

    public override void GetDamage(float damage)
    {
        Debug.Log($"Enemy damaged!");
        health.DeducHealth(damage);
        GameManager.GetInstance().scoreManager.IncrementScore();// moved from bulet script
        if (health.GetHealth() <= 0)
        {
            Die();
        }
    }
}