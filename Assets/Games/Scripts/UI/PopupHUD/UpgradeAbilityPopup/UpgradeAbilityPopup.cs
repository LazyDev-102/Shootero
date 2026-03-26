using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeAbilityPopup : BasePopup {
    [Header("Upgraded")]
    [SerializeField] private Image imgIcon;
    [SerializeField] private TextMeshProUGUI txtLevel;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtOldLevel;
    [SerializeField] private TextMeshProUGUI txtNewLevel;
    [SerializeField] private TextMeshProUGUI txtStatName;
    [SerializeField] private TextMeshProUGUI txtOldStat;
    [SerializeField] private TextMeshProUGUI txtNewStat;


    private AbilityData upgradedAbility;



    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
    }

    public void SetUpgradeAbility(AbilityData ability) {
        upgradedAbility = ability;
        ShowUpgradedInfo();
    }

    private void ShowUpgradedInfo() {
        imgIcon.sprite = upgradedAbility.Icon;
        txtLevel.text = upgradedAbility.IsMaxLevel ? "MAX" : (upgradedAbility.CurrentLevel + 1).ToString();
        txtName.text = upgradedAbility.AbilityName;
        txtOldLevel.text = $"Level {upgradedAbility.CurrentLevel}";
        txtNewLevel.text = $"Level {upgradedAbility.CurrentLevel + 1}";
        txtStatName.text = upgradedAbility.StatName;
        txtOldStat.text = upgradedAbility.GetValueString(upgradedAbility.CurrentLevel - 1);
        txtNewStat.text = upgradedAbility.GetValueString(upgradedAbility.CurrentLevel);
    }


}
