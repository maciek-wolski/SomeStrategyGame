using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private float flySpeed = 10.0f;
    private float attackDamage = 0.0f;
    private float projectileLifetime = 2.0f;
    [SerializeField] private Health targettedEnemyHealth = null;

#region Getters
#endregion
#region Setters
    public void SetAttackDamage(float newAttackDamage){
        attackDamage = newAttackDamage;
    }
    public void SetTargettedEnemy(Health newTargettedEnemy){
        targettedEnemyHealth = newTargettedEnemy;
    }
    public void SetProjectileLifetime(float newProjectileLifetime){
        projectileLifetime = newProjectileLifetime;
    }
#endregion

    private void Start() {
        Invoke(nameof(DestroyMe), projectileLifetime);
        rb.velocity = transform.forward * flySpeed;
    }
    private void DestroyMe(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        //checking if object has health component in it
        if (!other.TryGetComponent<Health>(out Health enemyHealth)){ return; }
        if (enemyHealth == targettedEnemyHealth){
            targettedEnemyHealth.TakeDamage(attackDamage);
            DestroyMe();
        }
    }

}
