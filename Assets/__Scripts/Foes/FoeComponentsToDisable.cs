using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FoeComponentsToDisable : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> _componentsToOff = new List<MonoBehaviour>();
    [SerializeField] private NavMeshAgent _agent = null;
    [SerializeField] private CapsuleCollider _collider = null;


    public void DisableComponents() {
        _agent.enabled = false;
        _collider.enabled = false;
        
        foreach (var component in _componentsToOff) {
            component.enabled = false;
        }
    }
}
