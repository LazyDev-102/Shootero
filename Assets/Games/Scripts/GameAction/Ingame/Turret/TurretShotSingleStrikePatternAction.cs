
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TurretShotSingleStrikePatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotSingleStrikePatternAction")]
public class TurretShotSingleStrikePatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private ShotSingleStrikePatternData data;
    public override void Execute(TurretBase target, object user, Action onCompleted) {
        Attack(target, onCompleted);
    }

    public override void Execute(TurretBase target, Action onCompleted) {
        Attack(target, onCompleted);
    }

    public override void RemoveExecute(TurretBase target, object user, Action onCompleted) {
    }
    private void Attack(TurretBase target, Action onCompleted) {
        var gameLoader = GameManager.Instance.GameLoader;
        var ship = gameLoader.Ship;
        var turretAttackComponent = target.TurretAttack.TurretAttackComponent;
        var speed = turretAttackComponent.TurretAtkSpeed.Value;
        var damage = target.TurretStat.GetFinalDamageWeapon;
        var spawnPos = turretAttackComponent.FirePoint.position;
        ShotSingleStrikePatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);
        Vector2 directionShot = turretAttackComponent.FirePoint.up;
        FrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
        if (goLeft) {
            goLeft.SetHitInfor(damage, null, target);
            goLeft.ChangeSpriteSize(patternInfo.BulletSize);
            goLeft.Shoot(speed, directionShot);
        }
    }
}
