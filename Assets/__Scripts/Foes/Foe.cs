using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foe : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Health health = null;
    [SerializeField] private int foePrice;
    [SerializeField] private int ownerId = -1;

    public static event Action<Foe> OnFoeDestroy; 
    public static event Action<Foe> OnFoeSpawned;

#region Getters
    public int GetFoeId(){
        return id;
    }
    public int GetOwnerId(){
        return ownerId;
    }
    public int GetFoePrice(){
        return foePrice;
    }

#endregion

#region Setters
    public void SetOwnerId(int newOwnerId){
        ownerId = newOwnerId;
    }

#endregion

    private void OnEnable() {
        OnFoeSpawned?.Invoke(this);
        health.OnDie += HandleOnDie;
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }

    private void HandleOnDie()
    {
        Debug.Log($"Foe {gameObject.name} died..");
        Destroy(gameObject);
    }

    private void OnDestroy() {
        OnFoeDestroy?.Invoke(this);
    }
}
