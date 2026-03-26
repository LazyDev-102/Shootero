using DG.Tweening;
using GameSystem.Common.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItemInfoPopup : DOTweenFrame {
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI abilityLevel;
    [SerializeField] private TextMeshProUGUI abilityDescription;
    [SerializeField] private TextMeshProUGUI currentValue;
    [SerializeField] private TextMeshProUGUI nextValue;
    [SerializeField] private TextMeshProUGUI currentPointText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI abilityPointText;
    [SerializeField] private ButtonExplorer upgradeButton;
    [SerializeField] private ButtonExplorer unlockButton;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private LockbarNotify lockbar;
    [SerializeField] private GameObject max;

    [Header("Ability Special")]
    [SerializeField] private GameObject unlockGroup;
    [SerializeField] private GameObject normalGroup;
    [SerializeField] private GameObject specialGroup;
    [SerializeField] private GameObject footerGroup;
    [SerializeField] private TextMeshProUGUI specialDescription;
    [SerializeField] private TextMeshProUGUI unlockValue;
    [SerializeField] private TextMeshProUGUI unlockDescription;

    [Header("Effect")]
    [SerializeField] private Image frameSelect;
    [SerializeField] private Image whiteFrame;
    [SerializeField] private float effectTime;

    private NewAbilityData abilityData;
    private NewAbilityItemData data;
    private Action onClose;

    private void Start() {
        upgradeButton.AddEvent(Upgrade);
        unlockButton.AddEvent(Upgrade);
        closeButton.AddEvent(Close);
    }

    public void Initialize(NewAbilityItemData data, Action onClose) {
        SetData(data, onClose);
        UpdateUI();
    }

    private void SetData(NewAbilityItemData data, Action onClose) {
        this.abilityData = GameResources.Instance.AbilityData;
        this.data = data;
        this.onClose = onClose;
    }

    private void UpdateUI() {
        bool isSpecial = data.IsSpecial;
        bool unlocked = data.Unlocked;
        bool isMax = data.IsMaxLevel;
        SetGroupStatus(isSpecial, unlocked && !isMax);
        SetButtonStatus(unlocked);
        SpecialUpdateUI(isSpecial);
        NormalUpdateUI(!isSpecial);
        LockGroupUpdateUI(!unlocked, isMax);
    }

    private void SetGroupStatus(bool isSpecial, bool unlocked) {
        unlockGroup.SetActive(!unlocked && !isSpecial);
        normalGroup.SetActive(!isSpecial && unlocked);
        specialGroup.SetActive(isSpecial);
        footerGroup.SetActive(!isSpecial || (isSpecial && !unlocked));
    }

    private void SetButtonStatus(bool unlocked) {
        lockbar.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(unlocked);
        unlockButton.gameObject.SetActive(!unlocked);
    }

    private void SpecialUpdateUI(bool status) {
        abilityLevel.gameObject.SetActive(!status);
        if (status) {
            specialDescription.text = data.Description;
            icon.sprite = data.Icon;
        }
    }

    private void NormalUpdateUI(bool status) {
        max.SetActive(data.IsMaxLevel);
        abilityPointText.text = GetAbilityPointText();
        if (status) {
            icon.sprite = data.Icon;
            abilityLevel.text = $"Lv.{data.Level}";
            abilityName.text = data.Name.ToUpper();
            abilityDescription.text = data.Description;
            currentValue.text = data.GetCurrentvalue();
            nextValue.text = data.GetNextvalue();
            currentPointText.text = $"{abilityData.Point}";
            priceText.text = $"{data.PointRequire}";
            upgradeButton.SetState(!data.IsMaxLevel);
        }
    }

    private string GetAbilityPointText() {
        return $"<color=\"green\">{abilityData.Point}</color>/{data.PointRequire} Ability Point";
    }

    private void LockGroupUpdateUI(bool status, bool isMax) {
        if (status || isMax) {
            unlockDescription.text = data.Description;
            unlockValue.text = isMax ? data.GetCurrentvalue() : data.GetUnlockValue();
        }
    }

    private void Upgrade() {
        if (!data.CanUnlock()) {
            lockbar.SetContent(GameDefine.UnlockPreviousAbility, 0.5f).Show();
        }
        else {
            abilityData.Upgrade(data.PointRequire, () => {
                data.LevelUp();
                PlayChooseEffect(effectTime);
                GameResources.Instance.DailyMission.AddPointProgress(MissionType.UpgradeAbility, 1);
                //UpdateUI();
            }, () => {
                lockbar.SetContent(GameDefine.InsufficientResources, 0.5f).Show();
            });
        }
    }
    public void PlayChooseEffect(float deltaTime) {
        HUDManager.IgnoreUserInput(true);
        transform.DOKill(true);
        whiteFrame.gameObject.SetActive(true);
        whiteFrame.SetAlpha(1);
        whiteFrame.transform.DOScale(Vector3.one * 2, deltaTime).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
            frameSelect.gameObject.SetActive(true);
            frameSelect.transform.DOScale(Vector3.one * 1.2f, deltaTime).SetUpdate(true).OnComplete(() => {
                whiteFrame.DOFade(0, deltaTime * 2).SetUpdate(true).OnComplete(() => {
                    UpdateUI();
                    HUDManager.IgnoreUserInput(false);
                });
                frameSelect.DOFade(0, deltaTime).SetUpdate(true);

            });
        });
    }
    private void Close() {
        onClose?.Invoke();
        onClose = null;
        OnBack();
    }
}
