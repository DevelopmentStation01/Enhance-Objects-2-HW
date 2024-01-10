using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health
{
    private float currentHealth;
    private float maxHealth;
    private float healthRegenerate;

    public Action<float> OnHealthUpdate;

    public float GetHealth()
    {
        return currentHealth;
    }

    public Health(float _maxHealth, float _healthRgenRate, float _currentHealth = 100)
    {
        currentHealth = _currentHealth;
        maxHealth = _maxHealth;
        healthRegenerate = _healthRgenRate;

        OnHealthUpdate?.Invoke(currentHealth);
    }

    public Health() { }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + value);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void DeducHealth(float value)
    {
        currentHealth = Mathf.Max(0, currentHealth - value);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public void RegenHealth()
    {
        AddHealth(healthRegenerate * Time.deltaTime);
    }
}
