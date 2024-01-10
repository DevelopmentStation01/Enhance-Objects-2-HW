using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPowerPickup : Pickup, IDamageable
{
    public override void OnPicked()
    {
        base.OnPicked();

        var player = GameManager.GetInstance().GetPlayer();
        player.gunPower.AddGunPower();
    }

    public void GetDamage(float damage)
    {
        OnPicked();
    }
}
