using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    // Setting timer to track bullet's life
    private float timeLimit = 2f;
    private float timer;

    private string targetTag;

    // Adding time limit in seconds to control bullet's life to decrease memory load
    private float timeLimit = 2f;
    private float timer;

    public void SetBullet(float _damage, string _targetTag, float _speed = 10)
    {
        this.damage = _damage;
        this.speed = _speed;
        this.targetTag = _targetTag;
    }

<<<<<<< HEAD
=======

>>>>>>> origin/tomo-dev
    private void Start()
    {
        timer = 0f; // Reset the timer when the bullet is created
    }

    private void Update()
    {
        Move();
        DestroyBullet();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

<<<<<<< HEAD
=======
    // Adding this method to destroy instantiated bullets by time, to decrease the system overloading
    private void DestroyBullet()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the time limit is reached
        if (timer >= timeLimit)
            Destroy(gameObject); // Destroy the bullet after the time limit
    }

>>>>>>> origin/tomo-dev
    private void Damage(IDamageable damageable)
    {
        if (damageable != null)
        {
            damageable.GetDamage(damage);
            Debug.Log("Damage something");
            //GameManager.GetInstance().scoreManager.IncrementScore();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if object has tag, not taking into account. Added to to not targeting itself
        if (!collision.gameObject.CompareTag(targetTag))
            return;

        Debug.Log($"Hit {collision.gameObject.name}");

        IDamageable damageable = collision.GetComponent<IDamageable>();
        Damage(damageable);
    }

    private void DestroyBullet()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the time limit is reached
        if (timer >= timeLimit)
        {
            // Destroy the bullet after the time limit
            Destroy(gameObject);
        }
    }
}
