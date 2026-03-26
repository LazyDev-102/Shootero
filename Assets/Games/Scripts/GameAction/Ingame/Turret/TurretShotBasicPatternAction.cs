using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretShotBasicPatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotBasicPatternAction")]
public class TurretShotBasicPatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bullet;
    public override void Execute(TurretBase target, object user, Action onCompleted) {
        Attack(target, onCompleted);
    }

    public override void Execute(TurretBase target, Action onCompleted) {
        Attack(target, onCompleted);
    }

    public override void RemoveExecute(TurretBase target, object user, Action onCompleted) {
    }
    private void Attack(TurretBase target, Action onCompleted) {
        if (target == null || bullet == null || target.TurretStat == null || target.TurretAttack == null)
            return;
        var attackComponent = target.TurretAttack.TurretAttackComponent;
        var gameLoader = GameManager.Instance.GameLoader;
        FrontBullet bulletClone = gameLoader.SpawnBullet(bullet, attackComponent.FirePoint.position);
        Vector2 direction = attackComponent.FirePoint.up;
        bulletClone.SetHitInfor(target.TurretStat.GetFinalDamageWeapon, null, target);
        bulletClone.Shoot(attackComponent.TurretAtkSpeed.Value, direction);
    }
}
