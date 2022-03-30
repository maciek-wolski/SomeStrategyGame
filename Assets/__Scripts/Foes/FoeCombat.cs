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
    [SerializeField] private Health enemyHealthScript = null;
    private float lastAttackTime = 0.0f;
    private void OnTriggerEnter(Collider other) {
        if (enemyHealthScript != null) { return; }
        //checking current target if its object with foe component in it then its already fighting
        GameObject currentTarget = foeMovement.GetTargetPoint().gameObject;
        if (currentTarget.TryGetComponent<Foe>(out Foe alreadyFighting)) { return; }
        //checking if object can be targeted
        if (!other.TryGetComponent<Foe>(out Foe enemyFoe)) { return; }
        //checking if it belongs to the same player
        if (enemyFoe.GetOwnerId() == foe.GetOwnerId()) { return; }
        //getting reference to enemy health script
        enemyHealthScript = other.gameObject.GetComponent<Health>();
        //setting target point to the founded enemy
        foeMovement.SetTargetPoint(other.gameObject.transform);
    }

    private void OnTriggerStay(Collider other) {
        if (enemyHealthScript != null) { return; }
        //checking current target if its object with foe component in it then its already fighting
        Transform currentTargetTransform = foeMovement.GetTargetPoint();
        if (currentTargetTransform == null) { return; }
        GameObject currentTarget = currentTargetTransform.gameObject;
        if (currentTarget.TryGetComponent<Foe>(out Foe alreadyFighting)) { return; }
        //checking if object can be targeted
        if (!other.TryGetComponent<Foe>(out Foe enemyFoe)) { return; }
        //checking if it belongs to the same player
        if (enemyFoe.GetOwnerId() == foe.GetOwnerId()) { return; }
        //getting reference to enemy health script
        enemyHealthScript = other.gameObject.GetComponent<Health>();
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
        }
        if (enemyHealthScript == null){
            foeMovement.SetTargetPoint(foeMovement.GetOriginalTargetPoint());
        }
    }

}
