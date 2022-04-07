using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCombat : MonoBehaviour
{
    [SerializeField] private Health myHealth = null;
    [SerializeField] private Tower tower = null;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private Transform rotatePoint = null;
    [SerializeField] private Transform spawnProjectile = null;
    [SerializeField] private float rotationSpeed = 500.0f;
    [SerializeField] private float attackDamage = 10.0f;
    [SerializeField] private float attackCooldown = 2.0f;
    private float lastAttackTime = 0.0f;
    [Tooltip("should be the same or bigger than detection range (sphere collider that is on trigger radius)")]
    [SerializeField] private float attackRange = 5.0f;

    private bool canAttack = false;

    // private void OnTriggerEnter(Collider other) {
    //     if (enemyHealthScript != null) { return; }
    //     //checking if object can be targeted
    //     if (!other.TryGetComponent<Health>(out Health enemyHealth)) { return; }
    //     //checking if it belongs to the same player or it's already destroyed
    //     if (enemyHealth.GetOwnerId() == tower.GetTowerHealth().GetOwnerId() || enemyHealth.GetCurrentHealth() <= 0) { return; }
    //     enemyHealthScript = enemyHealth;
    //     Invoke(nameof(SetCanAttack), 1.0f);
    // }

    // private void OnTriggerStay(Collider other) {
    //     if (enemyHealthScript != null) { return; }
    //     //checking if object can be targeted
    //     if (!other.TryGetComponent<Health>(out Health enemyHealth)) { return; }
    //     //checking if it belongs to the same player or it's already destroyed
    //     if (enemyHealth.GetOwnerId() == tower.GetTowerHealth().GetOwnerId() || enemyHealth.GetCurrentHealth() <= 0) { return; }
    //     enemyHealthScript = enemyHealth;
    //     Invoke(nameof(SetCanAttack), 1.0f);
    // }
    private void OnEnable() {
        myHealth.OnFoundEnemy += HandleOnFoundEnemy;
    }
    private void OnDisable() {
        myHealth.OnFoundEnemy -= HandleOnFoundEnemy;
    }

    private void HandleOnFoundEnemy(Transform obj)
    {
        Invoke(nameof(SetCanAttack), 1.0f);
    }

    private void SetCanAttack(){
        canAttack = true;
    }
    private void Update() {
        Health enemyHealth = myHealth.GetEnemyHealthScript();
        if (enemyHealth != null){
            //rotate attackPoint towards enemy
            Quaternion enemyRotation = 
                Quaternion.LookRotation(enemyHealth.GetAimAtPoint().position - rotatePoint.position);
            rotatePoint.rotation = 
                Quaternion.RotateTowards(rotatePoint.rotation, enemyRotation, rotationSpeed*Time.deltaTime);
            //checking distance
            Transform enemyTransform = enemyHealth.gameObject.transform;
            
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            if (distance > attackRange) { return; }
            
            //checking if can attack
            if (!canAttack) { return; }
            if ((lastAttackTime + attackCooldown) < Time.realtimeSinceStartup){
                GameObject newProjectile = projectilePrefab;
                Projectile projectile = newProjectile.GetComponent<Projectile>();
                projectile.SetAttackDamage(attackDamage);
                projectile.SetTargettedEnemy(enemyHealth);
                Instantiate(newProjectile, spawnProjectile.position, spawnProjectile.rotation);
                lastAttackTime = Time.realtimeSinceStartup;
            }
            
            if (enemyHealth.GetCurrentHealth() <= 0){
                myHealth.SetEnemyHealthScript(null);
                canAttack = false;
                return;
            }
        }
    }
}
