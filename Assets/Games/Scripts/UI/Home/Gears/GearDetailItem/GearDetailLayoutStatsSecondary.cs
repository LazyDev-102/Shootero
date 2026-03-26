using Gear_Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gemmob;
using DG.Tweening;

public class GearDetailLayoutStatsSecondary : MonoBehaviour, ILayout<GearDetailItemStatsSecondary, RankStat> {
    public List<GearDetailItemStatsSecondary> Items { get; set; } = new List<GearDetailItemStatsSecondary>();

    [SerializeField] private GearDetailItemStatsSecondary itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private bool isUpgradePanel = true;
    [SerializeField] private bool newItem = false;


    private List<RankStat> data;
    private int currentLevel;
    public void GenerateItem() {
        if (data == null && Items.Count == 0)
            return;
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
        DOVirtual.DelayedCall(1.9f, ActiveSpecial);
    }
    private void ActiveSpecial() {
        if (newItem) {
            for (int i = Items.Count - 1; i >= 0; i--) {
                if (Items[i].gameObject.activeInHierarchy) {
                    Items[i].InitializedSpecial();
                    break;
                }
            }
        }
    }
    public void UpdateUI(List<int> data, int currentLevel) {
        GetData(data);
        this.currentLevel = currentLevel;
        GenerateItem();
    }

    private void GetData(List<int> data) {
        this.data = new List<RankStat>();
        for (int i = 0; i < data.Count; i++) {
            this.data.Add(GameResources.Instance.GearData.RankStatData.GetRankStats(data[i]));
        }
    }
}
