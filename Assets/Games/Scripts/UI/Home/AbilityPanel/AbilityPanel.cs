using Gemmob;
using UnityEngine;
using GameSystem.Common.UI;
using TMPro;
using System;
using System.Linq;
using Helper;
using System.Collections;
using System.Collections.Generic;

public class AbilityPanel : DOTweenFrame {
    [SerializeField] private DOTweenAnimation showLeftToRight;
    [SerializeField] private DOTweenAnimation showRightToLeft;
    [SerializeField] private DOTweenAnimation hideLeftToRight;
    [SerializeField] private DOTweenAnimation hideRightToLeft;

    [SerializeField] private AbilityCollectionDisplayer normalCollectionDisplayer;
    [SerializeField] private AbilityItemView combineAbilityView;
    [SerializeField] private Transform contentLevelWarning;
    [SerializeField] private TextMeshProUGUI txtLevelWarning;
    [SerializeField] private ButtonBase btnUpgrade;
    [SerializeField] private ItemView priceUpgradeView;
    [SerializeField] private Transform highlightGraphic;
    [SerializeField] private AbilityInfoView abilityInfoView;
    [SerializeField] private float numberChoose;
    [SerializeField] private float deltaChoose;
    [SerializeField] private float acceleration;
    [SerializeField] private ButtonExplorer blackFrame;


    private AbilityCollectorData abilityCollectorData;
    private AbilityItemView abilityChoose;

    private void Start() {
        if (btnUpgrade) {
            btnUpgrade.AddEvent(OnUpgradeButtonClicked);
        }
        blackFrame.AddEvent(OnBlackButtonClick);
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        abilityCollectorData = GameResources.Instance.AbilityCollectorData;
        LoadData();
    }

    private void LoadData() {
        AbilityData[] normals = abilityCollectorData.NormalAbilityDatas;
        normalCollectionDisplayer.AddOnSelect(OnSelectedAbilityItemView).SetItems(normals).SetCapacity(normals.Length).Show();
        //combineAbilityView.SetModel(abilityCollectorData.CombineAbilityData).Show();


        bool hasPoint = abilityCollectorData.HasPointUpgrade;
        bool canUpgrade = abilityCollectorData.CanUpgrade;
        SetStateContentLevelWarning(!hasPoint);
        int curLevelGrade = GameResources.Instance.LevelProgress.GetCurrentLevel();
        SetContentLevelWarning($"{curLevelGrade + 2}", !hasPoint);
        SetStateUpgradeButton(canUpgrade, hasPoint);
        ItemStack price = new ItemStack(abilityCollectorData.PriceId, abilityCollectorData.PriceAmount);
        SetPriceUpgradeView(price, hasPoint);
        SetAbilityInfoView(null, false);
    }

    private void OnUpgradeButtonClicked() {
        GameResources.Instance.Inventory.Remove(abilityCollectorData.PriceId, abilityCollectorData.PriceAmount);
        StopAllCoroutines();
        StartCoroutine(IChoose());
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.UpgradeAbility, 1);
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnUpgradeAbility);
        ToolbarScaler.Instance.AbilityCheckNotify();
    }

    private void OnUpgradeAbilibyPopupClosed() {
        normalCollectionDisplayer.gameObject.SetActive(true);
    }

    public void SetStateContentLevelWarning(bool show) {
        if (contentLevelWarning) {
            contentLevelWarning.gameObject.SetActive(show);
        }
    }

    public void SetContentLevelWarning(string content, bool show = true) {
        if (txtLevelWarning) {
            txtLevelWarning.gameObject.SetActive(show);
            if (show) {
                txtLevelWarning.text = content;
            }
        }
    }

    public void SetStateUpgradeButton(bool interaction, bool show) {
        if (btnUpgrade) {
            btnUpgrade.gameObject.SetActive(show);
            if (show) {
                btnUpgrade.SetState(interaction);
            }
        }
    }

    public void SetPriceUpgradeView(ItemStack item, bool show) {
        if (priceUpgradeView && item != null) {
            priceUpgradeView.gameObject.SetActive(show);
            if (show) {
                priceUpgradeView.SetModel(item).Show();
            }
        }
    }


    public void SetAbilityInfoView(AbilityItemView displayer, bool show) {
        if (abilityInfoView) {
            abilityInfoView.transform.parent.gameObject.SetActive(show);
            abilityInfoView.gameObject.SetActive(show);
            if (show && displayer != null) {
                abilityInfoView.AddOnClose(OnAbilityInfoCloseClicked);
                abilityInfoView.transform.position = displayer.transform.position;
                abilityInfoView.SetModel(displayer.Model).Show();
            }
        }
    }

    private void OnAbilityInfoCloseClicked() {
        SetAbilityInfoView(null, false);
    }

    private void OnSelectedAbilityItemView(AbilityItemView displayer) {
        if (displayer != null && displayer.Model != null && displayer.Model.IsUnlocked) {
            SetAbilityInfoView(displayer, true);
        }
    }
    public override Frame SetAnimShow(bool leftToRight) {
        showAnimation = leftToRight ? showLeftToRight : showRightToLeft;
        return this;
    }
    public override Frame SetAnimHide(bool leftToRight) {
        hideAnimation = leftToRight ? hideLeftToRight : hideRightToLeft;
        return this;
    }

    private IEnumerator IChoose() {
        HUDManager.IgnoreUserInput(true);
        AbilityData upgradedAbility = abilityCollectorData.Upgrade();
        List<AbilityData> upgradeableAbilities = abilityCollectorData.GetAllAbility().ToList();
        upgradeableAbilities.Remove(upgradedAbility);
        AbilityData choosedAbility = null;
        for (int i = 0; i < numberChoose; ++i) {
            AbilityData curAbility = null;
            do {
                curAbility = RandomHelper.RandomInCollection(upgradeableAbilities);
            } while (choosedAbility == curAbility);
            choosedAbility = curAbility;
            AbilityItemView displayer = normalCollectionDisplayer.GetItemView(choosedAbility);
            if (displayer) {
                StartCoroutine(displayer.PlayEffect(0.3f));
                //SetHighlightGraphic(displayer.transform, true);
            }
            yield return Yielder.Wait(deltaChoose + i * acceleration);
        }
        abilityChoose = normalCollectionDisplayer.GetItemView(upgradedAbility);
        if (abilityChoose) {
            abilityChoose.PlayChooseEffect(deltaChoose * 2, UpgradeAbilityComplete);
            //SetHighlightGraphic(upgradeView.transform, true);
        }
        //yield return Yielder.Wait(2f);
        //HUDManager.IgnoreUserInput(false);
        //PopupHUD.Instance.Show<UpgradeAbilityPopup>().SetUpgradeAbility(upgradedAbility);
        //LoadData();
    }

    private void UpgradeAbilityComplete(AbilityItemView abilityItem) {
        HUDManager.IgnoreUserInput(false);
        LoadData();
        //blackFrame.gameObject.SetActive(true);
        abilityItem.InvokeOnSelect();
        abilityInfoView.SetInfoUpgradeBoard(true);
    }
    private void OnBlackButtonClick() {
        //blackFrame.gameObject.SetActive(false);
    }
    public void SetHighlightGraphic(Transform parent, bool show) {
        if (highlightGraphic) {
            highlightGraphic.gameObject.SetActive(show);
            if (show) {
                highlightGraphic.SetParent(parent);
                highlightGraphic.localPosition = Vector3.zero;
                highlightGraphic.SetAsFirstSibling();
                highlightGraphic.localScale = Vector3.one;
            }
        }
    }
}
