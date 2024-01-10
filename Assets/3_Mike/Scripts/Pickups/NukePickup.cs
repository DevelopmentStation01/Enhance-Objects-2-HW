using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NukePickup : Pickup, IDamageable
{
    public override void OnPicked()
    {
        base.OnPicked();

        var player = GameManager.GetInstance().GetPlayer();
        player.nuke.AddNuke();
    }

    public void GetDamage(float damage)
    {
        OnPicked();
    }
}
