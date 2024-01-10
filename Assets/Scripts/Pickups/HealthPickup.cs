using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup, IDamageable
{
    [SerializeField] private float healthMin, healthMax;
    
    public override void OnPicked()
    {
        base.OnPicked();

        float health = Random.Range(healthMin, healthMax);

        var player = GameManager.GetInstance().GetPlayer();
        player.health.AddHealth(health);
    }

    public void GetDamage(float damage)
    {
        OnPicked();
    }
}
