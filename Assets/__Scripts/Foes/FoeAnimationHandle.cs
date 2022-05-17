using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoeAnimationHandle : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    [SerializeField] private NavMeshAgent _agent = null;
    [SerializeField] private Health _myHealth = null;
    [SerializeField] private FoeMovement _movementScript = null;
    [SerializeField] private FoeComponentsToDisable _componentsToDisable = null;
    private int _attackHash, _dyingHash, _runHash, _idleHash;
    private Health _enemyHealth = null;
    private float _attackDamage = 0;

    private void OnEnable() {
        _myHealth.OnDie += TriggerDeathAnimation;
    }

    private void OnDisable() {
        _myHealth.OnDie -= TriggerDeathAnimation;
    }

    private void Start() {
        _attackHash = Animator.StringToHash("MeleeAttack");
        _dyingHash = Animator.StringToHash("Dying");
        _runHash = Animator.StringToHash("Run");
        _idleHash = Animator.StringToHash("Idle");
    }


#region Run
    public void TriggerRunAnimation() {
        _anim.SetBool(_runHash, true);
    }
#endregion

#region MeleeAttack
    public void TriggerAttackAnimation(Health enemyHealth, float attackDamage) {
        _anim.SetBool(_attackHash, true);
        _movementScript.canMove = false;
        _agent.velocity = Vector3.zero;
        _enemyHealth = enemyHealth;
        _attackDamage = attackDamage;
    }

    public void AnimDoDamage() {
        if (_enemyHealth == null) { return; } 
        _enemyHealth.TakeDamage(_attackDamage);
    }

    public void AnimEndOfAttackAnimation() {
        _movementScript.canMove = true;
        _anim.SetBool(_attackHash, false);
    }

#endregion

#region DeathAnim
    public void TriggerDeathAnimation() {
        _anim.SetBool(_dyingHash, true);
        _componentsToDisable.DisableComponents();
    }

    private void DestroyMe() {
        Destroy(gameObject);
    }
#endregion

    private void Update() {
        if (_anim.GetBool(_dyingHash)) { return; }
        if (_agent.velocity == Vector3.zero && !_anim.GetBool(_idleHash)) {
            _anim.SetBool(_runHash, false);
            _anim.SetBool(_idleHash, true);
        }
        else if (_agent.velocity != Vector3.zero && !_anim.GetBool(_runHash)) {
            _anim.SetBool(_runHash, true);
            _anim.SetBool(_idleHash, false);
        }
    }
}
