using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : CharacterAttack {
    private int droneId; // for get AC
    [SerializeField] private DroneAttackComponent currentAttackComponent;
    private bool canShot;

    private DroneBase droneBase;
    public DroneBase DroneBase {
        get {
            if (droneBase == null) {
                droneBase = CharacterBase as DroneBase;
            }
            return droneBase;
        }
    }

    public override void PreloadIngame() {
        if (currentAttackComponent) {
            currentAttackComponent.PreloadIngame();
        }
    }

    public override void Initialize() {
        base.Initialize();
        canShot = GameManager.Instance.GameLoader.Ship.ShipAttack.CanShot;
        AddAttackComponent();
    }

    public override void Destroy() {
        base.Destroy();
        StopAllCoroutines();
    }

    public override void Updating() {
        base.Updating();
        if (currentAttackComponent) {
            currentAttackComponent.Updating();
        }
    }

    public void AddAttackComponent() {
        currentAttackComponent.SetDroneAttack(this);
        currentAttackComponent.Initialize();
    }

    public void Attack() {
        if (canShot && currentAttackComponent) {
            currentAttackComponent.Attack();
        }
    }

    public void AddFireModifier(StatModifier fireRate, bool reset = true) {
        if (currentAttackComponent != null)
            currentAttackComponent.AddFireRateModifier(fireRate);
    }
    public void StartAttack() {
        if (currentAttackComponent) {
            currentAttackComponent.SetCanAttack(canShot);
        }
    }
    public void Reborn() {
        if (currentAttackComponent) {
            currentAttackComponent.Initialize();
            currentAttackComponent.SetCanAttack(canShot);
        }
    }
    public void ChangeStateShot(bool state) {
        canShot = state;
        if (currentAttackComponent) {
            currentAttackComponent.SetCanAttack(state);
        }
    }
}
