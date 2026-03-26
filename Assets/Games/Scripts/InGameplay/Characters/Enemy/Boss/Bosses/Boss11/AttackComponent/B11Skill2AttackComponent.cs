using UnityEngine;
using System;
using System.Collections.Generic;
using Gemmob;
using System.Collections;

public class B11Skill2AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B11Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private PierceFrontBullet bullet;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private int numberPreload;

    private bool canAim;
    AttackData attackData;
    private List<PierceFrontBullet> bullets;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Initialize() {
        base.Initialize();
        bullets = new List<PierceFrontBullet>();
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        if (bullets != null)
            bullets.Clear();
        atk = (int)(bossAttack.B11Base.B11Stat.Atk.Value * attackData.DamagePercent);
        canAim = true;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shoting());
    }

    private IEnumerator Shoting() {
        yield return Yielder.Wait(delayAttack);
        canAim = false;
        for (int i = 0; i < attackData.AttackCount; ++i) {
            if (muzzle) {
                muzzle.Play();
            }
            Vector2 directionShot = bossAttack.Target.position - transform.position;
            PierceFrontBullet centerBullet = GameLoader.SpawnBullet(bullet, transform.position);
            if (centerBullet) {
                centerBullet = ChangingBullet(centerBullet);
                centerBullet.SetSize(bossAttack.B11Base.B11Stat.Size.Value);
                centerBullet.Shoot(attackData.SpeedBullet, directionShot);
            }
            for (int ibullet = 0; ibullet < attackData.BulletCount / 2; ++ibullet) {
                Vector2 leftDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, attackData.SpreadAngle * (ibullet + 1));
                PierceFrontBullet leftBullet = GameLoader.SpawnBullet(bullet, transform.position);
                if (leftBullet) {
                    leftBullet = ChangingBullet(leftBullet);
                    leftBullet.SetSize(bossAttack.B11Base.B11Stat.Size.Value);
                    leftBullet.Shoot(attackData.SpeedBullet, leftDirectionShot);
                }

                Vector2 rightDirectionShot = Helper.GamePlayHelper.RotateDirection(directionShot, -1 * attackData.SpreadAngle * (ibullet + 1));
                PierceFrontBullet rightBullet = GameLoader.SpawnBullet(bullet, transform.position);
                if (rightBullet) {
                    rightBullet = ChangingBullet(rightBullet);
                    rightBullet.SetSize(bossAttack.B11Base.B11Stat.Size.Value);
                    rightBullet.Shoot(attackData.SpeedBullet, rightDirectionShot);
                }
            }
            canAim = true;
            yield return Yielder.Wait(attackData.DeltaShot);
            canAim = false;
        }
        EndAttack();
    }

    public override void EndAttack() {
        base.EndAttack();
    }

    private int atk;
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor((int)(atk * attackData.DamagePercent), null, bossAttack.BossBase);
        return bullet;
    }
    public override void StopAttack() {
        base.StopAttack();
    }

    public override void Updating() {
        if (canAim) {
            bossAttack.B11Base.LookTarget();
        }
    }

    [Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float speedBullet;
        [SerializeField] private float deltaShotTime;
        [SerializeField] private int bulletCount;
        [SerializeField] private int attackCount;
        [SerializeField] private int spreadAngle;

        public float DamagePercent {
            get => damagePercent;
        }
        public float DeltaShot {
            get => deltaShotTime;
        }
        public float SpeedBullet {
            get => speedBullet;
        }
        public int BulletCount {
            get => bulletCount;
        }
        public int AttackCount {
            get => attackCount;
        }
        public int SpreadAngle {
            get => spreadAngle;
        }
    }
}
