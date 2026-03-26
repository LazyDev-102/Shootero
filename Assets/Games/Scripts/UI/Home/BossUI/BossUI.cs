using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> ships;

    public void UpdateUI(int index) {
        if(ships == null) return;
        for(int i = 0; i < ships.Count; i++) {
            ships[i].SetActive(i == index);
        }
    }
}
