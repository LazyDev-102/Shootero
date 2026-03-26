using System.Collections.Generic;
using UnityEngine;
using GameSystem.Common.UI;
using Gear_Data;
using Gemmob;

public class AssignGearPopup : DOTweenFrame {
    [SerializeField] private GearItemView itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private ButtonExplorer closeButton;

    private GearSlotData data;
    private List<GearItemView> items = new List<GearItemView>();
    private void Awake() {
        closeButton.AddEvent(OnClose);
    }
    public void UpdateUI(GearSlotData data, List<GearSoftData> list) {
        this.data = data;
        Generate(list);
    }

    private void Generate(List<GearSoftData> list) {
        if (items.Count > list.Count) {
            for (int i = 0; i < items.Count; i++) {
                if (i < list.Count) {
                    items[i].UpdateUI(list[i], true);
                }
                items[i].gameObject.SetActive(i < list.Count && !list[i].IsEquiped);
            }
        }
        else {
            for (int i = 0; i < list.Count; i++) {
                if (i >= items.Count) {
                    var gearItem = itemPrefab.Spawn(container);
                    gearItem.transform.localPosition = Vector3.zero;
                    gearItem.transform.localScale = Vector3.one;
                    items.Add(gearItem);
                }
                items[i].UpdateUI(list[i], true);
                items[i].gameObject.SetActive(!list[i].IsEquiped);
            }
        }
    }

    private void OnClose() {
        Hide();
    }
}
