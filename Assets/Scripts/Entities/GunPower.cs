using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPower
{
    private float maxGunPower;
    private float currentGunPower;

    public Action<float> OnGunPowerUpdate;

    public float GetCurrentGunPower()
    {
        return currentGunPower;
    }

    public bool GetIsGunPower()
    {
        return currentGunPower > 0;
    }

    public GunPower(float _maxGunPower, float _currentGunPower)
    {
        this.currentGunPower = _currentGunPower;
        this.maxGunPower = _maxGunPower;
    }

    public void AddGunPower()
    {
        currentGunPower = maxGunPower;
        OnGunPowerUpdate?.Invoke(currentGunPower);
    }

    public void DeductGunPower(float value)
    {
        currentGunPower = Mathf.Max(0, currentGunPower - value);
        OnGunPowerUpdate?.Invoke(currentGunPower);
    }
}
