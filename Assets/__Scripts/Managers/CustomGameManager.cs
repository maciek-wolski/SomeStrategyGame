using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGameManager : MonoBehaviour
{
    private PlayerManager playerManager = null;

    private void Start() {
        playerManager = PlayerManager.instance;
        Foe.OnFoeDestroy += HandleDying;
    }

    private void OnDisable() {
        Foe.OnFoeDestroy -= HandleDying;
    }

    private void HandleDying(Foe foe)
    {
        Health foeHealth = foe.GetComponent<Health>();
        if (foeHealth.GetOwnerId() == playerManager.GetPlayerID()) { return; }
        playerManager.AddMoney(foe.GetFoeGoldIncome());
    }
    //wave2
    //enemy spawns 3x foe_01 and puts 1x tower_01

    //wave3
    //enemy spawns 1x (foe_1 + foe_02 + foe_03) and puts 1x tower_01 and 1x tower_03
}
