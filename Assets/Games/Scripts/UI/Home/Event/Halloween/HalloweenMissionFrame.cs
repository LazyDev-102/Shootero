using Gemmob;
using System.Collections.Generic;
using UnityEngine;

public class HalloweenMissionFrame : MonoBehaviour, ILayout<HalloweenMissionItem, HalloweenMissionItemData>
{
    [SerializeField] private HalloweenMissionItem itemPrefab;
    [SerializeField] private Transform container;

    private HalloweenMissionData data;
    public List<HalloweenMissionItem> Items { get; set; } = new List<HalloweenMissionItem>();

    public void Initialize(HalloweenMissionData data) {
        this.data = data;
    }

    public void GenerateItem() {
        for (int i = 0; i < data.Missions.Count; i++) {
            if (i >= Items.Count) {
                var itemClone = itemPrefab.Spawn(container);
                itemClone.transform.localPosition = Vector3.zero;
                itemClone.transform.localScale = Vector3.one;
                Items.Add(itemClone);
            }
            Items[i].Initialize(data.Missions[i]);
            Items[i].gameObject.SetActive(data.Missions[i].Active() ||( data.Missions[i].IsComplete && data.Missions[i].IsLastMission));
        }
    }

}
