using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1AttackComponent : TurretAttackComponent {
    [SerializeField] private FrontBullet bullet;
    [SerializeField, Range(0f, 100f)] float speedAttack;
    [SerializeField] private LayerMask enemyMask;

    private Countdowner fireRate = new Countdowner();
    private bool hasEnemy;
    private bool isRotation;

    public override void Initialize() {
        base.Initialize();
        TurretAtkSpeed.SetBaseValue(speedAttack);
        fireRate.StartCountdown(0);
    }

    public override void Attack() {
        if (fireRate.IsTimeOut()) {
            if (!FindEnemyTarget()) {
                if (!isRotation) {
                    isRotation = true;
                    TurretAttack.TurretBase.TurretMove.Rotation(true);
                }
                return;
            }
            base.Attack();
            TurretAttack.Shot();
            fireRate.StartCountdown(FireRate);
            if (isRotation) {
                isRotation = false;
                TurretAttack.TurretBase.TurretMove.Rotation(false);
            }
        }
        else {
            fireRate.Countdowning(Time.deltaTime);
        }
    }

    public override void Updating() {
        base.Updating();
        Attack();
    }

    private bool FindEnemyTarget() {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            TurretAttack.TurretBase.TurretMove.SetEnemyTarget(null);
            return false;
        }
        var radius = 3f;
        RaycastHit2D hit = default;
        while (hit == default && radius < 10) {
            hit = Physics2D.CircleCast(transform.position, radius, Vector2.up, 10f, enemyMask);
            radius += 1;
        }
        if (hit != default) {
            TurretAttack.TurretBase.TurretMove.SetEnemyTarget(hit.transform);
        }
        hasEnemy = hit != default;
        return hasEnemy;
    }

}
