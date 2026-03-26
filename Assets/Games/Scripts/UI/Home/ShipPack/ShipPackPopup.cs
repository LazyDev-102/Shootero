
using GameSystem.Common.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipPackPopup : DOTweenFrame {
    [SerializeField] private Transform container;
    [SerializeField] private ShipPackItem itemPrefab;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private ShipPackItem[] shipPacks;
    [SerializeField] private TextMeshProUGUI timeHappenText;

    private ShipPackInfo[] shipPackInfors;
    private ShipPackData data;

    private void Awake() {
        SetData();
        closeButton?.AddEvent(OnClose);
    }
    private void UpdateUI() {
        if (shipPackInfors == null)
            SetData();
        for (int i = 0; i < shipPacks.Length; i++) {
            if (i == shipPackInfors.Length)
                break;
            shipPacks[i].Initialized(shipPackInfors[i]);
        }
        timeHappenText.text = data.GetTimeHappen();
    }

    public void SetData() {
        data = GameResources.Instance.ShipPackData;
        shipPackInfors = data.Packs;
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
        scroll.verticalNormalizedPosition = 1;
    }
    private void OnClose() {
        Hide();
    }
}
