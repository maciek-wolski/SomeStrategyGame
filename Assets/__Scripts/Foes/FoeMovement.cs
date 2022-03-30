using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoeMovement : MonoBehaviour
{
    [SerializeField] private Transform targetPoint = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float enemyFoeDetectionRange = 6.0f;
    private Transform originalTargetPoint = null;

#region Getters
    public Transform GetTargetPoint(){
        return targetPoint;
    }
    public Transform GetOriginalTargetPoint(){
        return originalTargetPoint;
    }
#endregion
#region Setters
    public void SetTargetPoint(Transform newTargetPoint){
        if (targetPoint != null) {
            targetPoint = newTargetPoint;
            if (!agent.isActiveAndEnabled) { return; }
            agent.SetDestination(targetPoint.position);
            return;
        }
        targetPoint = newTargetPoint;
    }
#endregion

    private void Start() {
        originalTargetPoint = targetPoint;
        agent.SetDestination(targetPoint.position);
    }
}