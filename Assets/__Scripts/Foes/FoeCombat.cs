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

    //serialize field just to see it easier in the inspector while testing
    [SerializeField] private Health enemyHealthScript = null;
    private float lastAttackTime = 0.0f;
    private void OnTriggerEnter(Collider other) {
        if (enemyHealthScript != null) { return; }
        //checking current target if its object with foe component in it then its already fighting
        //checking if object can be targeted
        if (!other.TryGetComponent<Health>(out Health enemyHealth)) { return; }
        //checking if it belongs to the same player
        if (enemyHealth.GetOwnerId() == foe.GetFoeHealth().GetOwnerId() || enemyHealth.GetCurrentHealth() <= 0) { return; }
        //getting reference to enemy health script
        enemyHealthScript = other.gameObject.GetComponent<Health>();
        //setting target point to the founded enemy
        foeMovement.SetTargetPoint(other.gameObject.transform);
    }

    private void OnTriggerStay(Collider other) {
        if (enemyHealthScript != null) { return; }
        //checking current target if its object with foe component in it then its already fighting
        //checking if object can be targeted
        if (!other.TryGetComponent<Health>(out Health enemyHealth)) { return; }
        //checking if it belongs to the same player
        if (enemyHealth.GetOwnerId() == foe.GetFoeHealth().GetOwnerId() || enemyHealth.GetCurrentHealth() <= 0) { return; }
        //getting reference to enemy health script
        enemyHealthScript = enemyHealth;
        //setting target point to the founded enemy
        foeMovement.SetTargetPoint(other.gameObject.transform);
    }

    private void Update() {
        if (enemyHealthScript != null){
            foeMovement.SetTargetPoint(enemyHealthScript.gameObject.transform);
            if ((lastAttackTime + attackCooldown) < Time.realtimeSinceStartup){
                enemyHealthScript.TakeDamage(attackDamage);
                lastAttackTime = Time.realtimeSinceStartup;
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
