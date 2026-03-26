using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHealth : CharacterHealth {
    private DroneBase droneBase;
    public DroneBase DroneBase {
        get {
            if (droneBase == null) {
                droneBase = CharacterBase as DroneBase;
            }
            return droneBase;
        }
    }
    public override int CurrentHp {
        get {
            return currentHP;
        }
        protected set {
            int maxHp = DroneBase.DroneStat.GetFinalHPWeapon;
            currentHP = value;
            currentHP = Mathf.Clamp(currentHP, 0, maxHp);
            onHpChanged?.Invoke(currentHP, 1.0f * currentHP / maxHp);
        }
    }

    [SerializeField] private DroneHeathBar droneHealthBar;

    private DroneHeathBar droneHPBar;
    public DroneHeathBar DroneHPBar { get => droneHPBar; }
    public override void Initalize() {
        ForceChangeCurrentHp(DroneBase.DroneStat.GetFinalHPWeapon);
        LoadHealthBar();
    }
    public override void AddHpByPercent(float percent) {
        int hp = Mathf.CeilToInt(DroneBase.DroneStat.GetFinalHPWeapon * percent);
        CurrentHp += hp;
        TextShowupManager.Instance.ShowHealingText($"+ {hp}", DroneBase.DroneMove.MyRigi.position);
    }
    public override float GetPercentHPRemain() {
        return (float)((float)(currentHP) / (float)(DroneBase.DroneStat.GetFinalHPWeapon));
    }
    public void SelfDestroy() {
        if (droneHPBar != null) {
            droneHPBar.SelfDestroy();
            droneHPBar.FillFull();
            droneHPBar.RemoveListenerHealthChanged(DroneBase);
        }
    }
    public void LoadHealthBar() {
        if (droneHealthBar != null) {
            droneHPBar = droneHealthBar.Spawn(CommonHUD.Instance.transform);
            droneHPBar.SetTarget(DroneBase.transform)
                      .SetFollowTarget(DroneBase.DroneTopTrans)
                      .FillFull()
                      .AddListenerHealthChanged(DroneBase)
                      .gameObject.SetActive(true);
        }
    }
}
