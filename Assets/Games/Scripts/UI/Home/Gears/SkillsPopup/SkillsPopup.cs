using TMPro;
using Gemmob;
using System;
using UnityEngine;
using UnityEngine.UI;
using GameSystem.Common.UI;
using System.Collections.Generic;
using Gemmob.Tutorial;

public class SkillsPopup : DOTweenFrame, ILayout<SkillsItemView, ItemSkillData> {
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private SkillsItemView itemPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Transform selectedFrame;
    [SerializeField] private Image selectedIcon;
    [SerializeField] private TextMeshProUGUI selectedSkillName;
    [SerializeField] private TextMeshProUGUI selectedSkillTagName;
    [SerializeField] private TextMeshProUGUI selectedSkillDescription;
    [SerializeField] private ButtonExplorer equipButton;
    [SerializeField] private ButtonExplorer unequipButton;
    [SerializeField] private ButtonExplorer upgradeButton;
    [SerializeField] private ButtonExplorer openShopButton;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private GameObject[] activeStars;

    private ItemSkillData[] skillDatas;
    private SkillSystemData data;
    private ItemSkillData currentSkill;
    private SkillsItemView skillItemSelected;

    public List<SkillsItemView> Items { get; set; } = new List<SkillsItemView>();

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        ToolbarScaler.Instance.SetActive(false);
        base.OnShow(onCompleted, instant);
        SetData();
        GenerateItem();
        UpdateUI();
        ShowSkillsEquipTut();
        scrollRect.verticalNormalizedPosition = 1;
    }

    private void Awake() {
        equipButton.AddEvent(OnEquip);
        unequipButton.AddEvent(OnUnequip);
        upgradeButton.AddEvent(OpenSkillUpgradePopup);
        openShopButton.AddEvent(OpenShop);
        backButton.AddEvent(OnClose);
    }

    private void SetData() {
        if (data == null) {
            data = GameResources.Instance.SkillSystemData;
            skillDatas = data.AllSkills;
        }
        currentSkill = data.GetSkillSelected();
    }

    public void GenerateItem() {
        if (Items.Count > skillDatas.Length) {
            for (int i = 0; i < Items.Count; i++) {
                if (i < skillDatas.Length) {
                    Items[i].UpdateUI(skillDatas[i], OnSelected);
                }
                Items[i].gameObject.SetActive(i < skillDatas.Length);
            }
        }
        else {
            for (int i = 0; i < skillDatas.Length; i++) {
                if (i >= Items.Count) {
                    var itemClone = itemPrefab.Spawn(container);
                    itemClone.transform.localPosition = Vector3.zero;
                    itemClone.transform.localScale = Vector3.one;
                    Items.Add(itemClone);
                }
                Items[i].UpdateUI(skillDatas[i], OnSelected);
                Items[i].gameObject.SetActive(true);
            }
        }
    }

    private void UpdateUI() {
        if (currentSkill == null) {
            OnSelected(GetItemOwn());
        }

        bool unequip = currentSkill.IsEquip();
        bool isOwn = currentSkill.IsOwn;
        selectedIcon.sprite = currentSkill.Icon;
        selectedSkillName.text = currentSkill.Name;
        selectedSkillTagName.text = currentSkill.TagName;
        selectedSkillDescription.text = currentSkill.GetDescription(false);
        equipButton.gameObject.SetActive(isOwn && !unequip);
        unequipButton.gameObject.SetActive(isOwn && unequip);
        upgradeButton.gameObject.SetActive(isOwn);
        openShopButton.gameObject.SetActive(!isOwn);
        upgradeButton.SetState(isOwn && !currentSkill.IsMaxRank);
        UpdateActiveStarUI();
    }

    private SkillsItemView GetItemOwn() {
        foreach (var item in Items) {
            if (item.dataStack.IsOwn)
                return item;
        }
        return Items[0];
    }

    private void UpdateActiveStarUI() {
        if (currentSkill != null) {
            for (int i = 0; i < activeStars.Length; i++) {
                activeStars[i].SetActive(i <= currentSkill.Rank);
            }
        }
        else {
            for (int i = 0; i < activeStars.Length; i++) {
                activeStars[i].SetActive(false);
            }
        }
    }

    private void OnSelected(SkillsItemView selectedItem) {
        skillItemSelected = selectedItem;
        skillItemSelected.dataStack.IsNew = false;
        selectedFrame.SetParent(selectedItem.transform);
        selectedFrame.SetAsFirstSibling();
        selectedFrame.localPosition = Vector3.zero;
        currentSkill = selectedItem.dataStack;
        UpdateUI();
    }

    private void OnEquip() {
        data.AddSkill(currentSkill);
        GenerateItem();
        UpdateUI();
    }

    private void OnUnequip() {
        data.RemoveSkill();
        UpdateUI();
    }

    private void OpenShop() {
        PanelHUD.Instance.HideAll();
        ToolbarScaler.Instance.ShowShopPanel();
        PanelHUD.Instance.Shop.FocusSkill();
    }

    private void OpenSkillUpgradePopup() {
        PopupHUD.Instance.Show<SkillsUpgradePopup>()
                         .SetData(currentSkill, () => {
                             if (skillItemSelected != null)
                                 skillItemSelected.Generate();
                             GetItemOwn().Generate();
                             UpdateUI();
                         });
    }

    private void OnClose() {
        ToolbarScaler.Instance.SetActive(true);
        OnBack();
    }

    private void ShowSkillsEquipTut() {
        var tut = GameResources.Instance.TutorialSytemData.CanShowEquipSkillsTutorial();
        if (tut) {
            equipButton.gameObject.SetActive(true);
            TutorialSystem.Instance.SetTimeActiveCanvas(0.5f)
                                    .AssignTarget(TutorialKey.TutorialEquipSkills, 2, equipButton.gameObject);
        }
    }
}
