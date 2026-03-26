using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;

public class HB01RageAttackComponent : BossAttackComponent {
    [SerializeField] private HB01Attack bossAttack;
    [SerializeField] private BoomFrontBullet bullet;
    [SerializeField] private float boomRadius = 2;
    [SerializeField] private float acceleration = -1;
    [SerializeField] private Area spawnAre;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private int numberPreload;

    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.HB01Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.HB01Base.CurrentPhaseIndex];
        }
    }


    public override void PreloadIngame() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }


    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Shot());
    }

    private IEnumerator Shot() {
        for (int i = 0; i < attackData.NumberBullet; i++) {
            var pos = BorderHelper.GetWorldPointInsideArea(spawnAre);
            var bClone = GameManager.Instance.GameLoader.SpawnBullet(bullet, pos);
            bClone.SetHitInfor((int)(bossAttack.HB01Base.HB01Stat.Atk.Value * attackData.DamagePercent), null, bossAttack.HB01Base);
            bClone.SetMoveComplete(bClone.WarningEffect, 0)
                  .SetTarget((Vector3)pos + Vector3.up * 50)
                  .SetBoomRadius(boomRadius)
                  .Shoot(Vector2.down, attackData.BulletSpeed, acceleration);
            yield return Yielder.Wait(attackData.DeltaShot);
        }
        EndAttack();
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        bossAttack.BossBase.BossMove.EndMoveIdle();
    }

    public override void Updating() {

    }

    public override void StopAttack() {
        base.StopAttack();
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private int numberBullet = 5;
        [SerializeField] private float deltaShot = 0.5f;
        [SerializeField] private float damagePercent = 5;
        [SerializeField] private float bulletSpeed = 20;

        public int NumberBullet { get => numberBullet; }
        public float DeltaShot { get => deltaShot; }
        public float DamagePercent { get => damagePercent; }
        public float BulletSpeed { get => bulletSpeed; }
    }
}
