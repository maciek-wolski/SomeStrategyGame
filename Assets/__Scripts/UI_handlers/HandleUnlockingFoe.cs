using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleUnlockingFoe : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private GameObject lockedGameObject = null;
    [SerializeField] private GameObject unlockedGameObject = null;
    [SerializeField] private float unlockPrice = 50.0f;
    [SerializeField] private TMP_Text unlockTextPrice = null;
    private void Start() {
        unlockTextPrice.text = $"Unlock - {unlockPrice}$";
    }
    public void ButtonOnUnlockClick(){
        if ((playerManager.GetMyMoney() - unlockPrice) < 0) { return; }
        playerManager.ReduceMyMoney(unlockPrice);
        lockedGameObject.SetActive(false);
        unlockedGameObject.SetActive(true);
    }
}
