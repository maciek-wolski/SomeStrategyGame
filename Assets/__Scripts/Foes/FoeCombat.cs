using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeCombat : MonoBehaviour
{
    [SerializeField] private Health myHealth = null;
    [SerializeField] private Foe foe = null;
    [SerializeField] private FoeMovement foeMovement = null;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackDamage = 20.0f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private DetectEnemy detectEnemy = null;
    [SerializeField] private FoeAnimationHandle animationScript = null;
    
    private float lastAttackTime = 0.0f;
    
    private void OnEnable() {
        myHealth.OnFoundEnemy += HandleOnFoundEnemy;
    }
    private void OnDisable() {
        myHealth.OnFoundEnemy -= HandleOnFoundEnemy;
    }
    private void HandleOnFoundEnemy(Transform enemyTransform){
        foeMovement.SetTargetPoint(enemyTransform);
    }

    private void Update() {
        Health enemyHealth = myHealth.GetEnemyHealthScript();
        if (enemyHealth != null){
            Transform enemyHealthTransform = enemyHealth.gameObject.transform;
            float distance = Vector3.Distance(transform.position, enemyHealthTransform.position);
            foeMovement.SetTargetPoint(enemyHealthTransform);
            if ((lastAttackTime + attackCooldown) < Time.realtimeSinceStartup){
                if (distance <= attackRange){
                    DoDamage(enemyHealth);
                    lastAttackTime = Time.realtimeSinceStartup;
                }
            }
            if (enemyHealth.GetCurrentHealth() <= 0){
                myHealth.SetEnemyHealthScript(null);
                return;
            }
        }
        if (enemyHealth == null){
            foeMovement.SetTargetPoint(foeMovement.GetOriginalTargetPoint());
        }
    }

    private void DoDamage(Health enemyHealth) {
        lastAttackTime = Time.realtimeSinceStartup;
        if (animationScript == null) {
            enemyHealth.TakeDamage(attackDamage);
            return;
        }

        animationScript.TriggerAttackAnimation(enemyHealth, attackDamage);
    }
}