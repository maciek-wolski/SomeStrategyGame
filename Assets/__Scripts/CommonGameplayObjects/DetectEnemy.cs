using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    [SerializeField] private Health myHealth = null;

    private void OnTriggerEnter(Collider other) {
        if (myHealth.GetEnemyHealthScript() != null) { return; }
        //checking if object can be targeted
        if (!other.TryGetComponent<Health>(out Health enemyHealth)) { return; }
        //checking if it belongs to the same player
        if (enemyHealth.GetOwnerId() == myHealth.GetOwnerId() || enemyHealth.GetCurrentHealth() <= 0) { return; }
        //setting target point to the founded enemy
        myHealth.FoundEnemy(other.gameObject.transform, enemyHealth);
    }

    private void OnTriggerStay(Collider other) {
        if (myHealth.GetEnemyHealthScript() != null) { return; }
        //checking if object can be targeted
        if (!other.TryGetComponent<Health>(out Health enemyHealth)) { return; }
        //checking if it belongs to the same player
        if (enemyHealth.GetOwnerId() == myHealth.GetOwnerId() || enemyHealth.GetCurrentHealth() <= 0) { return; }
        //setting target point to the founded enemy
        myHealth.FoundEnemy(other.gameObject.transform, enemyHealth);
    }
}
