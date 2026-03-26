

using DG.Tweening;
using Gemmob;
using System.Collections;
using UnityEngine;

public class B11RageAttackComponent : BossAttackComponent {
    [SerializeField] private B11Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    //[SerializeField] private Laser bullet1;
    //[SerializeField] private Laser bullet2;
    [SerializeField] private Transform wingBulletPrefab;

    [SerializeField, Range(0f, 1000f)] private float rotateSpeed = 300f;
    [SerializeField, Range(0, 5)] private float radius = 3f;
    [SerializeField, Range(0, 50)] private int laserLength = 5;

    [SerializeField] private ParticleSystem appearWingLeftEffect;
    [SerializeField] private ParticleSystem appearWingRightEffect;
    [SerializeField, Tooltip("Speed Follow Ship(0 -> 1)")] private float smoothSpeed = 0.01f;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private Countdowner rateCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();

    private Vector3 smoothedPosition = Vector3.zero;
    private Transform wingBullet;
    private Laser bullet1;
    private Laser bullet2;
    private bool effectShowed;
    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B11Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B11Base.CurrentPhaseIndex];
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Initialize() {
        base.Initialize();
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        if (wingBullet == null) {
            wingBullet = wingBulletPrefab.Spawn(GameLoader.transform);
            wingBullet.gameObject.SetActive(false);
            bullet1 = wingBullet.GetChild(0).GetComponent<Laser>();
            bullet2 = wingBullet.GetChild(1).GetComponent<Laser>();
        }
        else {
            wingBullet.localPosition = Vector3.zero;
            bullet1.transform.localEulerAngles = Vector3.zero;
            bullet2.transform.localEulerAngles = Vector3.forward * 180;
        }
        effectShowed = false;
        StartBeamLaser();
        delayCountdowner.StartCountdown(delayAttack);

    }
    private void ShowEffect() {
        if (effectShowed)
            return;
        wingBullet.transform.position = bossAttack.transform.position;
        effectShowed = true;
        if (appearWingLeftEffect != null) {
            appearWingLeftEffect.Play();
        }
        if (appearWingRightEffect != null) {
            appearWingRightEffect.Play();
        }
        DOVirtual.DelayedCall(appearWingRightEffect.main.duration - 0.1f, () => wingBullet.gameObject.SetActive(true));
    }
    public override void Updating() {
        if (Time.timeScale == 0)
            return;
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            ShowEffect();
        }
        else {
            FollowShip();
            BeamingLaser();
        }
    }
    private void FollowShip() {
        bossAttack.B11Base.LookTarget();
        smoothedPosition = Vector3.Lerp(bossAttack.transform.position, bossAttack.Target.position, smoothSpeed);
        bossAttack.transform.position = smoothedPosition;
        wingBullet.transform.position = bossAttack.transform.position;
    }
    public override void Attacking() {
    }
    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(attackData.Duration);
        deltaShotCountdowner.StartCountdown(attackData.Duration);
        rateCountdowner.StartCountdown(0.5f);
        bullet1.StartBeam();
        bullet2.StartBeam();
        bullet1.SetRadiusSize(radius);
        bullet2.SetRadiusSize(radius);
        bullet1.SetMaxLength(laserLength);
        bullet2.SetMaxLength(laserLength);
        bullet1.gameObject.SetActive(true);
        bullet2.gameObject.SetActive(true);
        bullet1.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
        bullet2.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
    }

    public void BeamingLaser() {
        if (!durationCountdowner.IsTimeOut()) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            rateCountdowner.Countdowning(Time.deltaTime);

            if (rateCountdowner.IsTimeOut()) {
                Rotation();
            }
            if (deltaShotCountdowner.IsTimeOut()) {
                bullet1.Beaming(true);
                bullet2.Beaming(true);
                deltaShotCountdowner.StartCountdown(attackData.DeltaShot);
            }
            else {
                bullet1.Beaming(false);
                bullet2.Beaming(false);
            }
        }
        else {
            EndAttack();
        }
    }
    private void Rotation() {
        var temp = bullet1.transform.localEulerAngles;
        var temp2 = bullet2.transform.localEulerAngles;
        temp.z += Time.deltaTime * rotateSpeed;
        temp2.z += Time.deltaTime * rotateSpeed;
        bullet1.transform.localEulerAngles = temp;
        bullet2.transform.localEulerAngles = temp2;
    }
    private void EndBeamLaser() {
        bullet1.EndBeam();
        bullet2.EndBeam();
        bullet1.gameObject.SetActive(false);
        bullet2.gameObject.SetActive(false);
        wingBullet.Recycle();
        effectShowed = false;
    }

    public override void StopAttack() {
        EndBeamLaser();
        base.StopAttack();
    }

    public override void EndAttack() {
        EndBeamLaser();
        base.EndAttack();
    }
    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float duration;
        [SerializeField] private float deltaShot;

        public float DamagePercent { get => damagePercent; }
        public float Duration { get => duration; }
        public float DeltaShot { get => deltaShot; }
    }
}
