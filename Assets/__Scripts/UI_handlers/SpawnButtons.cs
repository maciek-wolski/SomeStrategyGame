using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpawnButtons : MonoBehaviour
{
    [Tooltip("Number of buyable foes up to 5!")]
    [SerializeField] private Foe[] buyableFoes = new Foe[5];
    [Tooltip("Number of foe buttons goes up to 5!")]
    [SerializeField] private List<FoeBuyButtonDisplay> foeButtonDisplays = new List<FoeBuyButtonDisplay>();

    private void Start() {
        if (buyableFoes.Length > 5 || foeButtonDisplays.Count > 5) {
            throw new Exception("size of buyableFoes and foeButtonDisplays cant go above 5!");
        }
        var sortedBuyableFoes = buyableFoes.OrderBy(x => x.GetFoePrice()).ToArray();
        buyableFoes = sortedBuyableFoes;
        for (int i = 0; i < buyableFoes.Length; i++){
            if (buyableFoes[i] == null) { break; }
            foeButtonDisplays[i].SetPriceText($"{buyableFoes[i].GetFoePrice().ToString()}$");
            foeButtonDisplays[i].SetFoeOnThisSpot(buyableFoes[i]);
        }
    }    
}
