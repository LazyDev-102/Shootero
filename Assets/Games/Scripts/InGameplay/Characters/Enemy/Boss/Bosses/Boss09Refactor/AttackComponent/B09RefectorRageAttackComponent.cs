
using UnityEngine;
using Gemmob;
using DG.Tweening;
using System.Collections;

public class B09RefectorRageAttackComponent : BossSkillAttackComponent {
    [SerializeField] private B09RefectorAttack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform shield;
    [SerializeField] private ParticleSystem trail1;
    [SerializeField] private ParticleSystem trail2;
    [SerializeField] private ParticleSystem trail3;
    [SerializeField] private BasicLaser laserLeft;
    [SerializeField] private BasicLaser laserCenter;
    [SerializeField] private BasicLaser laserRight;
    [SerializeField] private int laserLeftLength = 3;
    [SerializeField] private int laserCenterLength = 5;
    [SerializeField] private int laserRightLength = 3;
    [SerializeField] private ParticleSystem burstEffect;

    private bool activeAim;
    private bool isMoving;
    private int numberAttack = 0;
    private AttackData attackData;

    private Countdowner deltaShotCountdowner = new Countdowner();

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        activeAim = false;
        isMoving = false;
        numberAttack = 0;
    }
    public override void Updating() {
        if (activeAim) {
            bossAttack.B09RefectorBase.LookTarget();
        }
        if (isMoving) {
            bossAttack.B09RefectorBase.B09RefectorMove.MoveFront();
            if (!trail1.isPlaying && trail1 != null) {
                if (trail1 != null)
                    trail1.Play();
                if (trail2 != null)
                    trail2.Play();
                if (trail3 != null)
                    trail3.Play();
                if (burstEffect != null)
                    burstEffect.Play();
                StartBeamLaser();
            }
            BeamingLaser();
            if (bossAttack.B09RefectorBase.B09RefectorMove.HasOutBorder()) {
                isMoving = false;
                numberAttack++;
                var ranVector2 = new Vector2(0.5f, 1.1f);
                bossAttack.transform.position = bossAttack.B09RefectorBase.B09RefectorMove.GetPointMoveB09(ranVector2);
                ChangeZ(bossAttack.transform);
                if (numberAttack < attackData.AttackCount) {
                    activeAim = true;
                    DOVirtual.DelayedCall(0.1f, () => { activeAim = false; isMoving = true; });
                }
                else {
                    if (trail1 != null)
                        trail1.Stop();
                    if (trail2 != null)
                        trail2.Stop();
                    if (trail3 != null)
                        trail3.Stop();
                    var posDefault = new Vector2(0.5f, 0.8f);
                    bossAttack.transform.DOMove(bossAttack.B09RefectorBase.B09RefectorMove.GetPointMoveB09(posDefault), 2f).OnComplete(() => EndAttack());
                }
            }
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IDelayAttack());
    }

    public override void EndAttack() {
        base.EndAttack();
    }

    private IEnumerator IDelayAttack() {
        activeAim = true;
        yield return Yielder.Wait(attackData.AimTime);
        activeAim = false;
        isMoving = true;
        bossAttack.B09RefectorBase.B09RefectorMove.SetTargetMoveAttack(bossAttack.Target.position, attackData.MoveSpeed);
    }

    private void ChangeZ(Transform trans) {
        var temp = trans.rotation;
        temp.z = 180;
        trans.rotation = temp;
    }

    private void StartBeamLaser() {
        laserLeft.StartBeam();
        laserRight.StartBeam();
        laserCenter.StartBeam();
        laserLeft.SetMaxLength(laserLeftLength);
        laserRight.SetMaxLength(laserRightLength);
        laserCenter.SetMaxLength(laserCenterLength);
        laserLeft.gameObject.SetActive(true);
        laserRight.gameObject.SetActive(true);
        laserCenter.gameObject.SetActive(true);
    }
    private void EndBeamLaser() {
        laserLeft.EndBeam();
        laserRight.EndBeam();
        laserCenter.EndBeam();
        laserLeft.gameObject.SetActive(false);
        laserRight.gameObject.SetActive(false);
        laserCenter.gameObject.SetActive(false);
    }
    public void BeamingLaser() {
        if (isMoving) {
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            if (deltaShotCountdowner.IsTimeOut()) {
                laserLeft.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
                laserCenter.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
                laserRight.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
                laserLeft.Beaming(true);
                laserCenter.Beaming(true);
                laserRight.Beaming(true);
                deltaShotCountdowner.StartCountdown(attackData.DeltaShot);
            }
            else {
                laserLeft.Beaming(false);
                laserCenter.Beaming(false);
                laserRight.Beaming(false);
            }
        }
        else {
            EndBeamLaser();
        }
    }



    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float deltaShot;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackCount;
        [SerializeField] private float aimTime;

        public float DamagePercent {
            get => damagePercent;
        }
        public float MoveSpeed {
            get => moveSpeed;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float AttackCount {
            get => attackCount;
        }
        public float AimTime {
            get => aimTime;
        }
    }
}
