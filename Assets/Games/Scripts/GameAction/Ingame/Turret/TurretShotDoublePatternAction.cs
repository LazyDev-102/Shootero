using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretShotDoublePatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotDoublePattern")]
public class TurretShotDoublePatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private ShotDoublePatternData data;
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
        var directionShot = turretAttackComponent.FirePoint.up;

        ShotDoublePatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 positionLeft = (Vector2)turretAttackComponent.FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + (Vector2)directionShot * (ibullet * patternInfo.DistanceUpgradeY);
            FrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, positionLeft);
            if (goLeft) {
                goLeft.SetHitInfor(damage, null, target);
                goLeft.Shoot(speed, directionShot);
            }
            Vector2 positionRight = (Vector2)turretAttackComponent.FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + (Vector2)directionShot * (ibullet * patternInfo.DistanceUpgradeY);
            FrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, positionRight);
            if (goRight) {
                goRight.SetHitInfor(damage, null, target);
                goRight.Shoot(speed, directionShot);
            }
        }
    }
}
