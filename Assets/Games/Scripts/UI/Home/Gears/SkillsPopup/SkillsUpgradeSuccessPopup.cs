
using DG.Tweening;
using GameSystem.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUpgradeSuccessPopup : DOTweenFrame {
    [SerializeField] private float timeAppear = 1;
    [SerializeField] private float timeMove = 0.5f;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillTagName;
    [SerializeField] private TextMeshProUGUI skillDescription;
    [SerializeField] private ButtonExplorer closeButton;
    [SerializeField] private GameObject tabtoHideGO;

    private ItemSkillData skillsData;
    private SkillRankItemData data;

    private void Awake() {
        closeButton.AddEvent(OnClose);
    }
    public void InitData(ItemSkillData skillsData, SkillRankItemData data) {
        this.data = data;
        this.skillsData = skillsData;
        UpdateUI();
    }
    private void UpdateUI() {
        icon.sprite = skillsData.Icon;
        skillName.text = skillsData.Name;
        skillTagName.text = skillsData.TagName;
        skillDescription.text = skillsData.Description;
        HeadHUD.Instance.Hide<HeadPanel>();
    }
    private void OnClose() {
        Hide();
        HeadHUD.Instance.Show<HeadPanel>();
    }
    public override Frame OnBack() {
        OnClose();
        return this;
    }
}
