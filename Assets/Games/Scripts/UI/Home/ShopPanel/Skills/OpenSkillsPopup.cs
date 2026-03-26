
using GameSystem.Common.UI;
using Gemmob.Tutorial;
using Spine.Unity;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenSkillsPopup : BasePopup {
    [SerializeField] private TextMeshProUGUI txtTapOpen;
    [SerializeField] private TextMeshProUGUI priceOpenAgainText;
    [SerializeField] private SkeletonGraphic skeIconChest;
    [SerializeField] private Image skillIcon;
    [SerializeField] private ButtonBase btnTapOpen;
    [SerializeField] private ButtonBase btnOpenAgain;
    [SerializeField] private DotweenAnimation preOpenChest;
    [SerializeField] private DotweenAnimation openingChest;
    [SerializeField] private DotweenAnimation showSkill;
    [SerializeField] private TextMeshProUGUI txtSkillAmount;
    [SerializeField] private TextMeshProUGUI txtSkillDescription;
    [SerializeField] private TextMeshProUGUI skillType;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private string shakeWeak;
    [SerializeField] private GameObject afterOpen;
    [SerializeField] private LockbarNotify lockbarNotify;

    private TutorialSytemData tutData;
    private SkillSystemData data;
    private ItemSkillData skill;
    private int skillCount;
    private Action onClose;

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        SetStateTapOpenButton(true, true);
        onClose?.Invoke();
        base.OnHide(onCompleted, instant);
        ShowSkillsEquipTut();
    }
    public override Frame OnBack() {
        if (tutData.CanShowEquipSkillsTutorial())
            return this;
        else {
            return base.OnBack();
        }
    }

    protected override void Start() {
        base.Start();
        btnTapOpen.AddEvent(OnButtonTapOpenChestClicked);
        btnOpenAgain.AddEvent(OnButtonOpenAgainClicked);
        preOpenChest.Initialize();
        openingChest.Initialize();
        tutData = GameResources.Instance.TutorialSytemData;
    }

    public OpenSkillsPopup SetSkill(ItemSkillData skill) {
        this.skill = skill;
        return this;
    }
    public OpenSkillsPopup SetCount(int count) {
        skillCount = count;
        return this;
    }
    public OpenSkillsPopup SetData(SkillSystemData data) {
        this.data = data;
        return this;
    }
    public OpenSkillsPopup SetOnClose(Action onClose) {
        this.onClose = onClose;
        return this;
    }

    public void UpdateUI() {
        SetCloseState(true, false);
        SetStateOpenAgainButton(true, false);
        skeIconChest.gameObject.SetActive(true);
        skillIcon.gameObject.SetActive(false);
        skillIcon.sprite = skill.Icon;
        skillName.text = skill.Name;
        skillType.text = skill.TagName;
        txtSkillDescription.text = skill.GetDescriptionByIndex(0);
        txtSkillAmount.text = $"x{skillCount}";
        lockbarNotify.gameObject.SetActive(false);
    }
    private void OnButtonOpenAgainClicked() {
        ItemStack price = data.Pack.Price;
        GameResources.Instance.Inventory.EnoughPrice(price, OpenAgainActionSuccess, OpenAgainActionFail);
    }
    private void OpenAgainActionSuccess() {
        var count = data.GetRewardCount();
        var skill = data.GetRandomSkill();
        data.ClaimReward(skill, count);
        SetSkill(skill);
        SetCount(count);
        UpdateUI();
        OpenSkill();
        Tracking.Instance.LogShop(ShopButton.chest_skill);
    }
    private void OpenAgainActionFail() {
        ShowLockBarNotify(btnOpenAgain.transform);
    }
    private void OnButtonTapOpenChestClicked() {
        OpenSkill();
        txtTapOpen.gameObject.SetActive(false);
        SetStateTapOpenButton(true, false);
    }

    private void OpenSkill() {
        HUDManager.IgnoreUserInput(true);
        OpenSkill(OnOpenSkillComPlete);
    }
    private void OnOpenSkillComPlete() {
        SetStateOpenAgainButton(true, true);
        SetSkeletonIconChest(string.Empty, false);
        ShowSkill(OnShowSkillComplete);
    }
    private void OnShowSkillComplete() {
        HUDManager.IgnoreUserInput(false);
        SetCloseState(true, true);
    }
    private void OpenSkill(Action onComplete) {
        if (skeIconChest) {
            skeIconChest.AnimationState.SetAnimation(0, shakeWeak, false);
        }
        if (openingChest) {
            openingChest.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }
    private void ShowSkill(Action onComplete) {
        if (showSkill) {
            showSkill.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    private void SetSkeletonIconChest(string skin, bool show) {
        if (skeIconChest) {
            skeIconChest.gameObject.SetActive(show);
            if (show) {
                skeIconChest.Skeleton.SetSkin(skin);
                skeIconChest.Skeleton.SetSlotsToSetupPose();
                skeIconChest.LateUpdate();
            }
        }
    }
    private void SetStateTapOpenButton(bool interaction, bool show) {
        if (btnTapOpen) {
            btnTapOpen.gameObject.SetActive(show);
            if (show) {
                btnTapOpen.SetState(interaction);
            }
        }
    }
    private void SetStateOpenAgainButton(bool interaction, bool show) {
        if (btnOpenAgain) {
            btnOpenAgain.gameObject.SetActive(show);
            if (show) {
                btnOpenAgain.SetState(interaction);
                priceOpenAgainText.text = $"{data.Pack.Price.Amount}";
            }
        }
    }

    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1)
                     .SetContent(GameDefine.InsufficientResources, 0.5f)
                     .Show();
    }

    #region Tutorial
    private void ShowSkillsEquipTut() {
        if (tutData.CanShowEquipSkillsTutorial()) {
            TutorialSystem.Instance.SetTimeActiveCanvas(.1f)
                                   .GetData(TutorialKey.TutorialEquipSkills)
                                   .SetBackgroundButtonAlpha(0)
                                   .InitPointer(Vector3.one, 1f, "", 5)
                                   .AssignTarget(TutorialKey.TutorialEquipSkills, 0, ToolbarScaler.Instance.GetTabObject(ToolBarType.Gears))
                                   .ShowTutorial(OnCompleteEquipSkillTut);
        }
    }
    private void OnCompleteEquipSkillTut() {
        tutData.SetFinishTutorialEquipSkills(true);
    }
    #endregion
}
