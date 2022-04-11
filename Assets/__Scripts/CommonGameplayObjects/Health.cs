using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public Transform aimAtPoint = null;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float currentHealth = -1.0f;
    [SerializeField] private int ownerId = -1;
    [SerializeField] private Health enemyHealthScript = null;
    public event Action OnDie;
    public event Action<Transform> OnFoundEnemy;

#region Getters
    public float GetMaxHealth(){
        return maxHealth;
    }
    public float GetCurrentHealth(){
        return currentHealth;
    }
    public int GetOwnerId(){
        return ownerId;
    }
    public Transform GetAimAtPoint(){
        return aimAtPoint;
    }
    public Health GetEnemyHealthScript(){
        return enemyHealthScript;
    }

#endregion
#region Setters
    public void SetOwnerId(int newOwnerId){
        ownerId = newOwnerId;
    }
    public void SetEnemyHealthScript(Health newEnemyHealthScript){
        enemyHealthScript = newEnemyHealthScript;
    }
#endregion

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageValue) {
        if (currentHealth <= 0.0f) { return; }
        if (currentHealth > 0.0f) { currentHealth -= damageValue; }
        if (currentHealth > 0.0f) { return; }
        OnDie?.Invoke();
    }

    public void FoundEnemy(Transform enemyTransform, Health foundEnemyHealth) {
        enemyHealthScript = foundEnemyHealth;
        OnFoundEnemy?.Invoke(enemyTransform);
    }

    //just for testing
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            TakeDamage(50);
        }
    }
}