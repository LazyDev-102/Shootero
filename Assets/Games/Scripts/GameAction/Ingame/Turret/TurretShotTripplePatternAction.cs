using UnityEngine;
using System;

[CreateAssetMenu(fileName = "TurretShotTripplePatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotTripplePatternAction")]
public class TurretShotTripplePatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private ShotSplitterPatternData data;
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
        ShotSplitterPatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);
        Vector2 directionShot = turretAttackComponent.FirePoint.up;
        FrontBullet goMid = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
        if (goMid) {
            goMid.SetHitInfor(damage, null, target);
            goMid.Shoot(speed, directionShot);
        }
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet / 2; ++ibullet) {
            Vector2 directionLeft = Helper.GamePlayHelper.RotateDirection(directionShot, patternInfo.SpreadAngle * (ibullet + 1));
            FrontBullet goLeft = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
            if (goLeft) {
                goLeft.SetHitInfor(damage, null, target);
                goLeft.Shoot(speed, directionLeft);
            }
            Vector2 directionRight = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * patternInfo.SpreadAngle * (ibullet + 1));
            FrontBullet goRight = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
            if (goRight) {
                goRight.SetHitInfor(damage, null, target);
                goRight.Shoot(speed, directionRight);
            }
        }
    }
}
