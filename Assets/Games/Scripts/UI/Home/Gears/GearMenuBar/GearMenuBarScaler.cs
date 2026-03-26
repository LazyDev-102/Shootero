using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GearMenuBarScaler : MonoBehaviour {
    [SerializeField] private Button[] toolTab;
    [SerializeField] private float diffScale = 50, diffHigh = 10;
    [SerializeField] private Color backgroundNormal;
    [SerializeField] private Color backgroundOnFocus;
    [SerializeField] private List<GearMenuBarItem> menubarItems;
    [SerializeField] private Transform board;

    private float originY;
    private Vector2 origiSize;
    private GearMenuType cType = GearMenuType.Drone;
    public GearMenuType CType { get => cType; }
    public List<GearMenuBarItem> MenubarItems { get => menubarItems; }

    public void Init(Action<GearMenuType> onSelectTab) {
        origiSize = toolTab[0].rectTransform().sizeDelta;
        originY = menubarItems[0].GetOriginPosY();
        for (int i = 0; i < menubarItems.Count; i++) {
            menubarItems[i].OnSelectTab(onSelectTab);
        }
    }
    public void Assign(Action<GearMenuType> onSelectTab) {
        Init(onSelectTab);
        AssignEvent();
        OnTabClick(GearMenuType.Weapon);
    }
    private void AssignEvent() {
        toolTab[(int)GearMenuType.Weapon].onClick.AddListener(() => OnTabClick(GearMenuType.Weapon));
        toolTab[(int)GearMenuType.Shield].onClick.AddListener(() => OnTabClick(GearMenuType.Shield));
        toolTab[(int)GearMenuType.Core].onClick.AddListener(() => OnTabClick(GearMenuType.Core));
        toolTab[(int)GearMenuType.Engine].onClick.AddListener(() => OnTabClick(GearMenuType.Engine));
        toolTab[(int)GearMenuType.Drone].onClick.AddListener(() => OnTabClick(GearMenuType.Drone));
    }
    public void OnTabClick(GearMenuType type) {
        if (cType == type)
            return;
        ShowTab(type);
        cType = type;
    }
    private void ShowTab(GearMenuType type) {
        Scaler(type);
        foreach (var item in menubarItems) {
            item.OnSelect(type);
        }
    }
    public void Scaler(GearMenuType type) {
        var index = (int)type;
        var diff = diffScale / (toolTab.Length - 1);
        foreach (var b in toolTab) {
            b.rectTransform().sizeDelta = origiSize;
            toolTab[index].transform.GetChild(0).localPosition = Vector3.zero;
            b.DOKill();
        }
        for (int i = 0; i < toolTab.Length; i++) {
            if (i == index) {
                var size = origiSize;
                size.x += diffScale;
                toolTab[index].rectTransform().sizeDelta = size;
                SetStatusTabs(index);
            }
            else {
                var size1 = origiSize;
                size1.x -= diff;
                //toolTab[i].rectTransform().DOSizeDelta(size1, 0.3f);
                toolTab[i].rectTransform().sizeDelta = size1;
            }
        }
    }
    private void SetStatusTabs(int index) {
        for (int i = 0; i < toolTab.Length; i++) {
            menubarItems[i].MoveUpIcon(i == index, originY, diffHigh)
                    .ChangeTabNameStatus(i == index)
                    .ChangeAlphaIcon(i == index)
                    .ChangeAlphaBackground(i == index)
                    .ChangeBackground(i == index, backgroundOnFocus, backgroundNormal);
        }
    }
    public GameObject GetTabObject(GearMenuType type) {
        return toolTab[(int)type].gameObject;
    }
}
