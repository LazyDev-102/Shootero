using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretShotGunPatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotGunPatternAction")]
public class TurretShotGunPatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private ShotGunPatternData data;

    protected float leftAngle;
    protected float rightAngle;
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
        var damage = target.TurretStat.GetFinalDamageWeapon;
        var spawnPos = turretAttackComponent.FirePoint.position;
        Vector2 directionShot = turretAttackComponent.FirePoint.up;
        ShotGunPatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);
        FrontBullet middleBullet = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
        if (middleBullet) {
            middleBullet.SetHitInfor(damage, null, target);
            middleBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionShot);
        }
        leftAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            leftAngle -= patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, leftAngle);
            FrontBullet leftBullet = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
            if (leftBullet) {
                leftBullet.SetHitInfor(damage, null, target);
                leftBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
        rightAngle = 0;
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            rightAngle += patternInfo.Distance.GetRandomValue();
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, rightAngle);
            FrontBullet rightBullet = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
            if (rightBullet) {
                rightBullet.SetHitInfor(damage, null, target);
                rightBullet.Shoot(patternInfo.SpeedRange.GetRandomValue(), directionRandom);
            }
        }
    }
}
