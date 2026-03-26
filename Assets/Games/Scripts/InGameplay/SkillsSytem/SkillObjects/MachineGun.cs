using Gemmob;
using UnityEngine;

public class MachineGun : MonoBehaviour {
    [SerializeField] private FrontBullet bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Rigidbody2D rib;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float aimSpeed = 20;
    [SerializeField] private float offsetFirePoint = 0.3f;
    [SerializeField] private ParticleSystem spawnEffect;

    private float aimTime = 0.5f;
    private float deltaShot = 0.5f;
    private int damage = 0;
    private int bulletSpeed = 0;
    private bool canAttack;
    private bool hasEnemy;
    private Transform enemyTarget;
    private Countdowner deltaShotCd = new Countdowner();
    private Countdowner aimCd = new Countdowner();

    public void Preload() {
        if (spawnEffect != null)
            spawnEffect.RegisterPool(2);
    }

    public void StartAttack(int damage, float deltaShot, int bulletSpeed, float aimTime, float aimSpeed) {
        this.damage = damage;
        this.aimTime = aimTime;
        this.deltaShot = deltaShot;
        this.aimSpeed = aimSpeed;
        this.bulletSpeed = bulletSpeed;
        canAttack = true;
        PlayEffect();
    }

    public void EndAttack() {
        canAttack = false;
        PlayEffect();
    }
    private void PlayEffect() {
        if (spawnEffect != null) {
            GameManager.Instance.GameLoader.SpawnEffectExplosion(spawnEffect, transform.position);
        }
    }
    private void Update() {
        if (canAttack && Time.timeScale != 0 && GameManager.Instance.GameState == GameState.Playing) {
            if (enemyTarget == null && FindEnemyTarget())
                AimTarget();
            if (aimCd.IsTimeOut()) {
                if (deltaShotCd.IsTimeOut()) {
                    var bClone = GameManager.Instance.GameLoader.SpawnBullet(bulletPrefab, firePoint.position + Vector3.right * Random.Range(-offsetFirePoint, offsetFirePoint));
                    bClone.SetHitInfor(damage, null, null);
                    bClone.Shoot(bulletSpeed, transform.up);
                    aimCd.StartCountdown(aimTime);
                    SetEnemyTarget(null);
                    deltaShotCd.StartCountdown(deltaShot);
                }
                deltaShotCd.Countdowning(Time.deltaTime);
            }
            aimCd.Countdowning(Time.deltaTime);
        }
    }

    private void SetEnemyTarget(Transform target) {
        enemyTarget = target;
    }

    private bool FindEnemyTarget() {
        if (GameManager.Instance.GameLoader.Enemies.Count == 0) {
            SetEnemyTarget(null);
            return false;
        }
        var radius = 3f;
        RaycastHit2D hit = default;
        while (hit == default && radius < 10) {
            hit = Physics2D.CircleCast(transform.position, radius, Vector2.up, 10f, enemyMask);
            radius += 1;
        }
        if (hit != default) {
            SetEnemyTarget(hit.transform);
        }
        hasEnemy = hit != default;
        return hasEnemy;
    }

    private void AimTarget() {
        rib.MoveRotation(Mathf.LerpAngle(rib.rotation, Vector2.SignedAngle(Vector2.up, enemyTarget.position - transform.position), Time.deltaTime * aimSpeed));
    }
}
