
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TurretShotTwistedPlasmaPatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotTwistedPlasmaPatternAction")]
public class TurretShotTwistedPlasmaPatternAction : TurretPatternAction {
    [SerializeField] private SinFrontBullet bulletPrefab;
    [SerializeField] private ShotTwistedPlasmaPatternData data;
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
        ShotTwistedPlasmaPatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);
        Vector2 directionShot = turretAttackComponent.FirePoint.up;
        int length = patternInfo.NumberBullet / 2;
        if (length <= 0)
            return;
        for (int ibullet = 0; ibullet < length; ++ibullet) {
            Vector2 positionLeft = (Vector2)turretAttackComponent.FirePoint.position + Vector2.left * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            SinFrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, positionLeft);
            if (goLeft) {
                goLeft.SetHitInfor(damage, null, target);
                goLeft.Shoot(speed, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet), false);
            }
            Vector2 positionRight = (Vector2)turretAttackComponent.FirePoint.position + Vector2.right * (patternInfo.HalfDistanceBase + ibullet * patternInfo.DistanceUpgradeX) + Vector2.up * (ibullet * patternInfo.DistanceUpgradeY);
            SinFrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, positionRight);
            if (goRight) {
                goRight.SetHitInfor(damage, null, target);
                goRight.Shoot(speed, directionShot, patternInfo.GetAmplitude(ibullet), patternInfo.GetCycle(ibullet));
            }
        }
    }
}
