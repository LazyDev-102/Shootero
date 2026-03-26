using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretShotGatlingPatternAction", menuName = "Resource/GameAction/TurretPattern/TurretShotGatlingPatternAction")]
public class TurretShotGatlingPatternAction : TurretPatternAction {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private ShotGatlingPatternData data;
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
        var attackComponent = target.TurretAttack.TurretAttackComponent;
        var speed = attackComponent.TurretAtkSpeed.Value;
        var damage = target.TurretStat.GetFinalDamageWeapon;
        var spawnPos = attackComponent.FirePoint.position;
        Vector2 directionShot = attackComponent.FirePoint.up;
        ShotGatlingPatternInfo patternInfo = data.GetPatternByLevelIndex(ship.ShipAttack.CurrentLevelBulletUp);
        for (int ibullet = 0; ibullet < patternInfo.NumberBullet; ++ibullet) {
            Vector2 directionRandom = Helper.GamePlayHelper.RotateDirection(directionShot, UnityEngine.Random.Range(-patternInfo.SpreadAngle, patternInfo.SpreadAngle));
            FrontBullet bullet = gameLoader.SpawnBullet(bulletPrefab, spawnPos);
            if (bullet) {
                bullet.SetHitInfor(damage, null, target);
                bullet.Shoot(speed, directionRandom);
            }
        }
    }
}
