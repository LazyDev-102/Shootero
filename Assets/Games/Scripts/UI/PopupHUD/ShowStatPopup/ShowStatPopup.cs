using System;
using TMPro;
using UnityEngine;

public class ShowStatPopup : BasePopup {
    [SerializeField] private TextMeshProUGUI txtAttack;
    [SerializeField] private TextMeshProUGUI txtHp;
    [SerializeField] private TextMeshProUGUI txtFireRateAdd;
    [SerializeField] private TextMeshProUGUI txtBlastDamage;
    [SerializeField] private TextMeshProUGUI txtBlastRadius;
    [SerializeField] private TextMeshProUGUI txtBlockDamage;
    [SerializeField] private TextMeshProUGUI txtBulletSize;
    [SerializeField] private TextMeshProUGUI txtBulletSpeed;
    [SerializeField] private TextMeshProUGUI txtBurnDamage;
    [SerializeField] private TextMeshProUGUI txtBurnStack;
    [SerializeField] private TextMeshProUGUI txtBurnTime;
    [SerializeField] private TextMeshProUGUI txtChipGain;
    [SerializeField] private TextMeshProUGUI txtExpGain;
    [SerializeField] private TextMeshProUGUI txtColliderDamage;
    [SerializeField] private TextMeshProUGUI txtCritDamage;
    [SerializeField] private TextMeshProUGUI txtCritRate;
    [SerializeField] private TextMeshProUGUI txtDamageRed;
    [SerializeField] private TextMeshProUGUI txtEvasion;
    [SerializeField] private TextMeshProUGUI txtRecoverHP;


    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        PlayerStatManager stats = PlayerStatManager.Instance;
        txtAttack.text = stats.Damage.ToString();
        txtHp.text = stats.HP.ToString();
        txtFireRateAdd.text = stats.FireRate * 100 + "%";
        txtBlastDamage.text = stats.BlastDamage * 100 + "%";
        txtBlastRadius.text = stats.BlastRadius * 100 + "%";
        txtBlockDamage.text = stats.BlockDamage + "%";
        txtBulletSize.text = stats.BulletSize * 100 + "%";
        txtBulletSpeed.text = stats.BulletSpeed.ToString();
        txtBurnDamage.text = stats.BurnDamage * 100 + "%";
        txtBurnStack.text = stats.BurnStack.ToString();
        txtBurnTime.text = stats.BurnTime * 100 + "%";
        txtChipGain.text = stats.Chip * 100 + "%";
        txtExpGain.text = stats.Exp * 100 + "%";
        txtColliderDamage.text = stats.ColliderDamage * 100 + "%";
        txtCritDamage.text = stats.CritDamage * 100 + "%";
        txtCritRate.text = stats.CritRate + "%";
        txtDamageRed.text = stats.DamageReduction * 100 + "%";
        txtEvasion.text = stats.DodgeRate + "%";
        txtRecoverHP.text = stats.RecoverHP * 100 + "%";
    }
}
