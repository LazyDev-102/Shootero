using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionProgressLayout : MonoBehaviour, ILayout<DailyMissionProgressItem, DailyMissionProgressItemData> {
    [SerializeField] private Image icon;
    [SerializeField] private Image progressImage;
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private DailyMissionProgressItem itemPrefab;

    private DailyMissionProgressItemData[] datas;
    public List<DailyMissionProgressItem> Items { get; set; } = new List<DailyMissionProgressItem>();

    public void GenerateItem() {
        if (Items != null && Items.Count > datas.Length) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < datas.Length) {
                    Items[i].UpdateUI(datas[i]);
                }
                Items[i].gameObject.SetActive(i < datas.Length);
            }
        }
        else {
            for (int i = 0; i < datas.Length; i++) {
                if (Items == null || i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].UpdateUI(datas[i]);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    public void UpdateUI(DailyMissionProgressItemData[] datas, int progress, int target) {
        this.datas = datas;
        progressText.text = $"{progress}";
        Progress((float)progress / (float)target);
        GenerateItem();
    }
    public void Progress(float value) {
        var duration = value - progressImage.fillAmount;
        progressImage.DOFade(1, duration).SetEase(Ease.Linear).OnUpdate(() => {
            progressImage.fillAmount += Time.deltaTime;
        }).OnComplete(() => progressImage.fillAmount = value);
    }
}
