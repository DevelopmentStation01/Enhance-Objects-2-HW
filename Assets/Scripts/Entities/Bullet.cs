using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private string targetTag;

    public void SetBullet(float _damage, string _targetTag, float _speed = 10)
    {
        this.damage = _damage;
        this.speed = _speed;
        this.targetTag = _targetTag;
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void Damage(IDamageable damageable)
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
}
