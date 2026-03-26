using UnityEngine;

public class ShipAttack : CharacterAttack {
    [SerializeField] private ShipAttackComponent startAttackComponent;
    [SerializeField] private ShipAttackComponent currentAttackComponent;
    [HideInInspector] public ShotPatternType PatternType;

    private readonly int maxLevelBulletUp = 3;
    private int currentLevelBulletUp;
    private int shipIndex;
    private bool isForceMaxBulletUp;
    private bool canShot;

    public int ShipIndex {
        get {
            return shipIndex;
        }
    }
    private ShipBase shipBase;
    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = CharacterBase as ShipBase;
            }
            return shipBase;
        }
    }

    public int CurrentLevelBulletUp { get => isForceMaxBulletUp ? maxLevelBulletUp : currentLevelBulletUp; }
    public ShipAttackComponent CurrentAttackComponent { get => currentAttackComponent; }
    public bool CanShot { get => canShot; }


    public override void PreloadIngame() {
        if (startAttackComponent) {
            startAttackComponent.PreloadIngame();
        }
    }

    public override void Initialize() {
        base.Initialize();
        canShot = false;
        currentLevelBulletUp = 0;
        shipIndex = GameResources.Instance.Ship.CurrentShip - 1;
        AddAttackComponent(startAttackComponent);
    }

    public override void Destroy() {
        base.Destroy();
        StopAllCoroutines();
    }

    public void Revive() {

    }

    public override void Updating() {
        base.Updating();
        if (currentAttackComponent) {
            currentAttackComponent.Updating();
        }
    }

    public void AddAttackComponent(ShipAttackComponent attackComponent) {
        if (attackComponent == null)
            return;
        currentAttackComponent = Instantiate(attackComponent, transform);
        currentAttackComponent.SetShipAttack(this);
        currentAttackComponent.Initialize();
    }

    public void Attack() {
        if (canShot && currentAttackComponent) {
            currentAttackComponent.Attack();
        }
    }

    public void BulletUp() {
        currentLevelBulletUp++;
        if (currentAttackComponent) {
            currentAttackComponent.BulletUp();
        }
    }

    public void Focus() {
        currentAttackComponent.FocusUpgrade();
    }

    public void SetForceMaxLevelBulletUp(bool status) {
        isForceMaxBulletUp = status;
    }

    public void ChangePattern(ShipPattern shipPattern) {
        if (currentAttackComponent) {
            currentAttackComponent.ChangeToPattern(shipPattern);
        }
    }
    public void ChangeStateShot(bool state) {
        canShot = state;
        DroneManager.Instance.ChangeShotStatus(state);
    }
}

