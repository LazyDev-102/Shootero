using GameSystem.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class SkillsUpgradePopup : DOTweenFrame {
    [SerializeField] private Image icon;
    [SerializeField] private Image progressIcon;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillTagName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private TextMeshProUGUI skillAmount;
    [SerializeField] private ButtonExplorer fuseButton;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private ButtonExplorer tabCloseButton;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Transform starEffect;

    private ItemSkillData data;
    private Action onClose;

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(() => {
            onCompleted?.Invoke();
            tabCloseButton.interactable = true;
        }, instant);
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        onClose?.Invoke();
    }

    private void Awake() {
        fuseButton.AddEvent(OnFuse);
        closeButton.AddEvent(OnClose);
        tabCloseButton.AddEvent(OnClose);
    }

    public void SetData(ItemSkillData data, Action onClose) {
        this.data = data;
        this.onClose = onClose;
        UpdateUI();
    }

    private void UpdateUI() {
        starEffect.gameObject.SetActive(false);
        icon.sprite = data.Icon;
        progressIcon.sprite = data.Icon;
        skillName.text = data.Name;
        skillTagName.text = data.TagName;
        skillDescription.text = data.GetDescription(true);
        skillAmount.text = data.GetAmountDescription();
        fuseButton.SetState(data.CanUpgradable());
        for (int i = 0; i < stars.Length; i++) {
            stars[i].SetActive(i <= data.Rank);
        }
    }

    private void OnFuse() {
        data.Upgrade();
        PlayEffect();
    }

    private void OnClose() {
        tabCloseButton.interactable = false;
        OnBack();
    }

    private void PlayEffect() {
        fuseButton.SetState(false);
        starEffect.SetParent(stars[data.Rank].transform.parent);
        starEffect.gameObject.SetActive(true);
        starEffect.localScale = Vector3.zero;
        starEffect.localPosition = Vector3.zero;
        starEffect.DOScale(Vector3.one, 1f)
                  .OnComplete(UpdateUI)
                  .SetAutoKill(true);
    }
}
