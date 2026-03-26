using DG.Tweening;
using Gemmob;
using Gemmob.Tutorial;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GearContainer : MonoBehaviour, ILayout<GearItemView, GearSoftData> {
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Transform container;
    [SerializeField] private GearItemView itemPrefab;
    [SerializeField] private Vector3 itemScale;

    private List<GearSoftData> data;
    public List<GearItemView> Items { get; set; } = new List<GearItemView>();
    public Transform Container { get => container; }

    public void Reload() {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
    public void UpdateUI(List<GearSoftData> data) {
        this.data = data;
        GenerateItem();
        ShowTutorial();
    }
    public void GenerateItem() {
        if (Items != null && Items.Count > data.Count) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < data.Count) {
                    Items[i].UpdateUI(data[i], false);
                }
                Items[i].gameObject.SetActive(i < data.Count);
            }
        }
        else {
            for (int i = 0; i < data.Count; i++) {
                if (Items == null || i >= Items.Count) {
                    var gearItem = itemPrefab.Spawn(container);
                    gearItem.transform.localPosition = Vector3.zero;
                    gearItem.transform.localScale = itemScale;
                    Items.Add(gearItem);
                }
                Items[i].UpdateUI(data[i], false);
                Items[i].gameObject.SetActive(true);
            }
        }
    }
    private void ShowTutorial() {
        ShowEquipmentTut();
    }
    private void ShowEquipmentTut() {
        var tut = GameResources.Instance.TutorialSytemData.FinishTutorialEquipment;
        scrollRect.enabled = tut;
        if (!tut) {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                    .AssignTarget(TutorialKey.TutorialEquipment, 2, GetGearItemTutorial());
        }
    }
    private GameObject GetGearItemTutorial() {
        if (container.childCount > 0)
            return container.GetChild(0).gameObject;
        Logs.Log("Not found Item");
        return null;
    }
}
