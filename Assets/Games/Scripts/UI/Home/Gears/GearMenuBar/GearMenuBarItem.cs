using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class GearMenuBarItem : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Image noticeIcon;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMPro.TextMeshProUGUI mainName;
    [SerializeField] private GearMenuType gearMenuType = GearMenuType.Weapon;
    public GearMenuType GearMenuType { get => gearMenuType; }
    private Action<GearMenuType> onSelectTab;

    public GearMenuBarItem OnSelectTab(Action<GearMenuType> onSelectTab) {
        this.onSelectTab = onSelectTab;
        return this;
    }

    public void OnSelect(GearMenuType type) {
        if (type != gearMenuType)
            return;
        onSelectTab?.Invoke(type);

    }

    public GearMenuBarItem MoveUpIcon(bool isSelect, float origin, float diffHigh) {
        //icon.transform.DOLocalMoveX(isSelect ? origin + diffHigh : origin, 0.5f);
        icon.transform.DOLocalMoveX(isSelect ? origin + diffHigh : origin, 0.001f);
        return this;
    }

    public GearMenuBarItem ChangeAlphaIcon(bool isSelect) {
        icon.SetAlpha(isSelect ? 1 : 0.2f);
        return this;
    }
    public GearMenuBarItem ChangeAlphaBackground(bool isSelect) {
        backgroundImage.SetAlpha(isSelect ? 1 : 0.2f);
        return this;
    }


    public GearMenuBarItem ChangeTabNameStatus(bool active) {
        mainName.gameObject.SetActive(active);
        return this;
    }

    public GearMenuBarItem ChangeBackground(bool isSelect, Color colorSelect, Color colorDeselect) {
        backgroundImage.color = isSelect ? colorSelect : colorDeselect;
        return this;
    }

    public float GetOriginPosY() {
        return icon.transform.localPosition.y;
    }

    public void SetNotification(bool value) {
        noticeIcon.gameObject.SetActive(value);
    }
}
