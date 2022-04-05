using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeCombat : MonoBehaviour
{
    [SerializeField] private Foe foe = null;
    [SerializeField] private FoeMovement foeMovement = null;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float attackDamage = 20.0f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private DetectEnemy detectEnemy = null;
    //serialize field just to see it easier in the inspector while testing
    [SerializeField] private Health enemyHealthScript = null;
    
    private float lastAttackTime = 0.0f;

#region Getters
    public Health GetEnemyHealthScript(){
        return enemyHealthScript;
    }
#endregion
    
    public void FoundTarget(Transform target, Health enemyHealth){
        enemyHealthScript = enemyHealth;
        foeMovement.SetTargetPoint(target);
    }

    private void Update() {
        if (enemyHealthScript != null){
            float distance = Vector3.Distance(transform.position, enemyHealthScript.gameObject.transform.position);
            foeMovement.SetTargetPoint(enemyHealthScript.gameObject.transform);
            if ((lastAttackTime + attackCooldown) < Time.realtimeSinceStartup){
                if (distance <= attackRange){
                    enemyHealthScript.TakeDamage(attackDamage);
                    lastAttackTime = Time.realtimeSinceStartup;
                }
            }
            if (enemyHealthScript.GetCurrentHealth() <= 0){
                enemyHealthScript = null;
                return;
            }
        }
        if (enemyHealthScript == null){
            foeMovement.SetTargetPoint(foeMovement.GetOriginalTargetPoint());
        }
    }
}