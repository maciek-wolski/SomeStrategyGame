using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform targetPoint = null;
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float enemyFoeDetectionRange = 6.0f;

    private void Start() {
        agent.SetDestination(targetPoint.position);
    }
}