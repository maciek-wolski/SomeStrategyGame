using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private Health health = null;
    [SerializeField] private int towerPrice;
    [SerializeField] private int ownerId = -1;

    public static event Action<Tower> OnTowerSpawned;
    public static event Action<Tower> OnTowerDestroyed;

    private void Start() {
        OnTowerSpawned?.Invoke(this);
    }

    private void OnDestroy() {
        OnTowerDestroyed?.Invoke(this);
    }
}
