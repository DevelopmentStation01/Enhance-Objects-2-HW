using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private string name;
    private float damage;
    private float bulletSpeed;

    //Creating CONSTRUCTOR, it looks like a method but with the name of a class
    public Weapon(string _name, float _damage, float _bulletSpeed)
    {
        this.name = _name;
        this.damage = _damage;
        this.bulletSpeed = _bulletSpeed;
    }

    public Weapon() { }

    public void Shoot(Bullet _bullet, PlayableObject _player, string _targetTag, float _timeToDie = 5)
    {
        Debug.Log($"Shooting from gun");

        Bullet bulletObj = GameObject.Instantiate(_bullet, _player.transform.position, _player.transform.rotation);
        bulletObj.SetBullet(damage, _targetTag, bulletSpeed);

        GameObject.Destroy(bulletObj.gameObject, _timeToDie);
    }

    public float GetDamage()
    {
        return damage;
    }
}
