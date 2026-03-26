using Gear_Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gemmob;

public class GearDetailLayoutStatsPrimary : MonoBehaviour, ILayout<GearDetailItemStatsPrimary, LevelStatData> {
    public List<GearDetailItemStatsPrimary> Items { get; set; } = new List<GearDetailItemStatsPrimary>();

    [SerializeField] private GearDetailItemStatsPrimary itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private bool isUpgradePanel = true;


    private List<LevelStatData> data;
    private int currentLevel;
    public void GenerateItem() {
        if (Items != null && Items.Count > data.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < data.Count) {
                    Items[i].Initialized(data[i], currentLevel, isUpgradePanel);
                }
                Items[i].gameObject.SetActive(i < data.Count);
            }
        }
        else {
            for (int i = 0; i < data.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var skinClone = itemPrefab.Spawn(container);
                    skinClone.transform.localPosition = Vector3.zero;
                    skinClone.transform.localScale = Vector3.one;
                    Items.Add(skinClone);
                }
                Items[i].Initialized(data[i], currentLevel, isUpgradePanel);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    public void UpdateUI(List<LevelStatData> data, int currentLevel) {
        this.data = data;
        this.currentLevel = currentLevel;
        GenerateItem();
    }

}
