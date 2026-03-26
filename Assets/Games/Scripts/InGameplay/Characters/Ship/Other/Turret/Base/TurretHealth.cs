using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretHealth : CharacterHealth {
    private TurretBase turretBase;
    public TurretBase TurretBase {
        get {
            if (turretBase == null) {
                turretBase = CharacterBase as TurretBase;
            }
            return turretBase;
        }
    }

    [SerializeField] private TurretHealthBar playerHealthBar;

    private TurretHealthBar turretHPBar;
    public TurretHealthBar TurretHPBar { get => turretHPBar; }
    //private IEnumerator UpdateProgressHPBar(int hp, float pct) {
    //    if (hpProgress.fillAmount < pct) {
    //        while (pct != 0 && hpProgress.fillAmount < pct) {
    //            hpProgress.fillAmount += Time.deltaTime;
    //            yield return null;
    //        }
    //    }
    //    else {
    //        while (pct != 0 && hpProgress.fillAmount > pct) {
    //            hpProgress.fillAmount -= Time.deltaTime;
    //            yield return null;
    //        }
    //    }
    //}

    //private void Awake() {
    //    AddOnHpChanged(OnHpChanged);
    //}
    //private void OnDestroy() {
    //    RemoveOnHpChanged(OnHpChanged);
    //}
    public override void Initalize() {
        base.Initalize();
        LoadHealthBar();
    }
    //public void OnHpChanged(int hp, float ratio) {
    //    StartCoroutine(UpdateProgressHPBar(hp, ratio));
    //}

    public void SelfDestroy() {
        if (turretHPBar != null) {
            turretHPBar.SelfDestroy();
            turretHPBar.Recycle();
            turretHPBar.RemoveListenerHealthChanged(TurretBase);
        }
    }
    public void LoadHealthBar() {
        if (playerHealthBar) {
            turretHPBar = playerHealthBar.Spawn(CommonHUD.Instance.transform);
            turretHPBar.SetTarget(TurretBase.transform);
            turretHPBar.FillFull();
            turretHPBar.AddListenerHealthChanged(TurretBase);
            turretHPBar.gameObject.SetActive(true);
        }
    }
}
