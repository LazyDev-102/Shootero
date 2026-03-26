using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GearMaterialContainer : MonoBehaviour {
    [SerializeField] private ItemCollector materialCollector;
    [SerializeField] private List<GearMaterialItemView> gearMatrial;
    public void UpdateUI() {
        for (int i = 0; i < gearMatrial.Count; i++) {
            gearMatrial[i].InitData(materialCollector.Items[i], GameResources.Instance.Inventory.GetItem(materialCollector.Items[i].Id).Amount);
        }
    }
}
