using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FoeBuyButtonDisplay : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager = null;
    [SerializeField] private TMP_Text priceText = null;
    private Foe foeOnThisSpot;

#region Getters
    public TMP_Text GetPriceText(){
        return priceText;
    }
    public Foe GetFoeOnThisSpot(){
        return foeOnThisSpot;
    }
#endregion

#region Setters
    public void SetPriceText(string newPriceText){
        priceText.text = $"{newPriceText}";
    }
    public void SetFoeOnThisSpot(Foe foe){
        foeOnThisSpot = foe;
    }
#endregion

    public void ButtonSpawnFoe(){
        playerManager.SpawnFoe(foeOnThisSpot.GetFoeId());
    }
}
