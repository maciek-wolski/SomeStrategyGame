using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    public event Action OnDie;

#region Getters
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

#endregion

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageValue){
        if (currentHealth <= 0.0f) { return; }
        if (currentHealth > 0.0f) { currentHealth -= damageValue; }
        if (currentHealth > 0.0f) { return; }
        OnDie?.Invoke();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(50);
        }
    }
}