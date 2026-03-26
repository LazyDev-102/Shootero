

using System.Collections;
using UnityEngine;
using Gemmob;

public class B05RageAttackComponent : BossAttackComponent {
    [SerializeField] private B05Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Laser bullet;
    [SerializeField] private BasicLaser waring;
    [SerializeField, Range(0f, 1f)] private float timeOffLaserPercent = 1;
    [SerializeField] private float radius;
    [SerializeField] private float warningRadius;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private bool attacking;
    private bool endAttack;
    private bool hasRotate;

    private Countdowner delayCountdowner = new Countdowner();

    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B05Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B05Base.CurrentPhaseIndex];
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        bullet.SetRadiusSize(radius);
        attacking = false;
        endAttack = false;
    }
    public override void Updating() {
        if (!attacking) {
            bossAttack.B05Base.B05Move.MoveDirect();
            Rotation();
            attacking = bossAttack.B05Base.B05Move.CompleteMoveToTarget();
        }
        else {
            BeamingLaser();
        }
    }

    public override void Attacking() {
        StartBeamLaser();
        SetTimeCountdown();
        bossAttack.B05Base.B05Move.StartMoveAfterAttackB05(new Vector2(0.5f, 0.8f));
    }
    private void SetTimeCountdown() {
        delayCountdowner.StartCountdown(attackData.DelayTime);
    }
    private void StartBeamLaser() {
        bullet.StartBeam();
        bullet.gameObject.SetActive(true);
    }

    public void BeamingLaser() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            //Rotation();
        }
        else {
            if (!endAttack) {
                //hasRotate = false;
                endAttack = true;
                if (gameObject.activeInHierarchy)
                    StartCoroutine(BulletAttack());
            }
        }
    }

    private IEnumerator BulletAttack() {
        for (int i = 0; i < attackData.AttackStack; i++) {
            waring.gameObject.SetActive(true);
            var time = 0f;
            while (time < attackData.TimePerAttack) {
                time += Time.deltaTime;
                bossAttack.B05Base.LookTarget();
                yield return null;
            }
            waring.gameObject.SetActive(false);
            for (int j = 0; j < attackData.StackPerAttack; j++) {
                bullet.gameObject.SetActive(true);
                bullet.SetPercentSize(1);
                var temp = bullet.transform.localEulerAngles;
                temp.z = Random.Range(attackData.StartAnglePoint, attackData.EndAnglePoint);
                bullet.transform.localEulerAngles = temp;
                bullet.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
                bullet.Beaming(true);
                float timeOffPoint = attackData.TimeLife * (1 - timeOffLaserPercent);
                float timeOffPoint1 = attackData.TimeLife * (1 - timeOffLaserPercent);
                yield return Yielder.Wait(attackData.TimeLife - timeOffPoint);
                while (timeOffPoint > 0) {
                    bullet.SetPercentSize(timeOffPoint / timeOffPoint1);
                    bullet.Beaming(false);
                    timeOffPoint -= Time.deltaTime;
                    yield return null;
                }
                bullet.gameObject.SetActive(false);
                yield return Yielder.Wait(attackData.TimePerShot);
            }
            //yield return Yielder.Wait(timePerAttack);
        }

        EndAttack();
        EndBeamLaser();
    }
    private void Rotation() {
        //if (hasRotate)
        //    return;
        //hasRotate = true;
        //bossAttack.transform.localEulerAngles = Vector3.Lerp(bossAttack.transform.localEulerAngles, Vector3.forward * 180, Time.deltaTime * 1000);
        bossAttack.transform.localEulerAngles = Vector3.Lerp(bossAttack.transform.localEulerAngles, Vector3.forward * 180, 0.125f);
    }
    private void EndBeamLaser() {
        bullet.gameObject.SetActive(false);
        waring.gameObject.SetActive(false);
        bullet.EndBeam();
    }
    public override void EndAttack() {
        EndBeamLaser();
        base.EndAttack();
    }
    public override void StopAttack() {
        EndBeamLaser();
        base.StopAttack();
    }

    private void OnDisable() {
        EndBeamLaser();
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float delayTime;
        [SerializeField] private float timeLife;
        [SerializeField] private int stackPerAttack = 5;
        [SerializeField] private int attackStack;
        [SerializeField] private float timePerShot = 1;
        [SerializeField] private float timePerAttack = 2;
        [SerializeField] private int startAnglePoint = -30;
        [SerializeField] private int endAnglePoint = 30;
        [SerializeField] private float damagePercent = 1;

        public float DelayTime { get => delayTime; }
        public float TimeLife { get => timeLife; }
        public int StackPerAttack { get => stackPerAttack; }
        public int AttackStack { get => attackStack; }
        public float TimePerShot { get => timePerShot; }
        public float TimePerAttack { get => timePerAttack; }
        public int StartAnglePoint { get => startAnglePoint; }
        public int EndAnglePoint { get => endAnglePoint; }
        public float DamagePercent { get => damagePercent; }
    }
}
