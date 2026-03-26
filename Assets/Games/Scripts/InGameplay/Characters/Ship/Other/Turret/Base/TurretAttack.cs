using System.Linq;
using UnityEngine;

public class TurretAttack : CharacterAttack {
    private int TurretId; // for get AC
    [SerializeField] private TurretAttackComponent currentAttackComponent;
    [SerializeField] private TurretPatternAction[] turretActions;
    private TurretPatternAction curretPattern;

    private TurretBase turretBase;
    public TurretBase TurretBase {
        get {
            if (turretBase == null) {
                turretBase = CharacterBase as TurretBase;
            }
            return turretBase;
        }
    }

    public TurretAttackComponent TurretAttackComponent { get => currentAttackComponent; }

    public override void Initialize() {
        base.Initialize();
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
        currentAttackComponent.SetTurretAttack(this);
        currentAttackComponent.Initialize();
    }

    public void Attack() {
        if (currentAttackComponent) {
            currentAttackComponent.Attack();
        }
    }

    public void AddFireModifier(FloatStat time) {
        if (currentAttackComponent != null)
            currentAttackComponent.AddFireRateModifier(time);
    }
    public void ChangePattern(ShipBase ship) {
        curretPattern = turretActions.FirstOrDefault(x => x.PatternType == ship.ShipAttack.PatternType);
        if (curretPattern == null)
            curretPattern = turretActions[0];
    }
    public void Shot() {
        if (curretPattern == null)
            curretPattern = turretActions[0];
        curretPattern.Execute(TurretBase);
    }
}
