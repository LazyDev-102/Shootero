using DG.Tweening;
using UnityEngine;

public class B11Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B11Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Laser bullet;
    [SerializeField] private Laser warningLine1;
    [SerializeField] private ParticleSystem[] bulletEffect;
    [SerializeField] private int[] bulletLength;

    [SerializeField, Range(0, 5)] private float radius = 3f;
    [SerializeField, Range(0f, 1f)] private float timeOffWarningLaserPercent = 0.5f;
    [SerializeField] private int warningMaxStack = 4;
    [SerializeField, Range(0f, 1f)] private float warningAlpha = 0.5f;

    private Countdowner deltaShotCountdowner = new Countdowner();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner delayCountdowner = new Countdowner();
    private Countdowner endAttackCD = new Countdowner();
    AttackData attackData;
    private float warningTimeOffPoint;
    private int warningStack = 0;
    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        playingEffect = false;
        StartBeamLaser();
        delayCountdowner.StartCountdown(delayAttack);
        endAttackCD.StartCountdown(0.1f);
        bullet.SetMaxLength(bulletLength[CurrentPhaseIndex]);
        bullet.StartBeam();
        warningLine1.SetMaxLength(bulletLength[CurrentPhaseIndex]);
        warningLine1.StartBeam();
        warningLine1.SetAlphaLaser(warningAlpha);
        warningStack = 0;

    }

    private void DrawWarning() {
        if (delayCountdowner.Countdown < warningTimeOffPoint) {
            float percentSize = warningTimeOffPoint == 0 ? 1 : delayCountdowner.Countdown / warningTimeOffPoint;
            warningLine1.SetPercentSize(percentSize);
            if (warningStack % warningMaxStack == 0) {
                warningLine1.SetAlphaLaser((warningStack / warningMaxStack) % 2 == 0, maxValue: warningAlpha);
            }
            warningStack++;
        }
        warningLine1.gameObject.SetActive(true);
        warningLine1.Beaming(false);
    }

    private void HideWarning() {
        warningLine1.gameObject.SetActive(false);

    }
    private bool playingEffect;
    public override void Updating() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            DrawWarning();
            bossAttack.B11Base.LookTarget();
            if (delayCountdowner.IsTimeOut()) {
                HideWarning();
            }
        }
        else {
            BeamingLaser();
            if (!playingEffect) {
                PlayEffectBullet();
            }
        }
    }
    public override void Attacking() {
        bossAttack.B11Base.B11Move.StopMoveIdle();
        warningLine1.SetMaxLength(bulletLength[CurrentPhaseIndex]);
    }
    private void StartBeamLaser() {
        durationCountdowner.StartCountdown(attackData.Duration);
        deltaShotCountdowner.StartCountdown(0);
        bullet.StartBeam();
        bullet.SetRadiusSize(radius);
        bullet.gameObject.SetActive(true);
        warningTimeOffPoint = delayAttack * (1 - timeOffWarningLaserPercent);
    }

    public void BeamingLaser() {
        if (!durationCountdowner.IsTimeOut()) {
            durationCountdowner.Countdowning(Time.deltaTime);
            deltaShotCountdowner.Countdowning(Time.deltaTime);
            if (deltaShotCountdowner.IsTimeOut()) {
                bullet.SetInfor((int)(bossAttack.CharacterBase.CharacterStat.Atk.Value * attackData.DamagePercent), null);
                bullet.Beaming(true);
                deltaShotCountdowner.StartCountdown(attackData.DeltaShot);
            }
            else {
                bullet.Beaming(false);
            }
        }
        else {
            if (endAttackCD.IsTimeOut()) {
                EndAttack();
            }
            else {
                endAttackCD.Countdowning(Time.deltaTime);
            }
        }
    }
    private void EndBeamLaser() {
        bullet.EndBeam();
        bullet.gameObject.SetActive(false);
        warningLine1.gameObject.SetActive(false);
    }
    private void PlayEffectBullet() {
        playingEffect = true;
        DOVirtual.DelayedCall(0.1f, () => {
            if (gameObject.activeInHierarchy)
                bulletEffect[CurrentPhaseIndex].Play();
        });
    }
    public override void StopAttack() {
        bossAttack.B11Base.B11Move.RestartMoveIdle();
        EndBeamLaser();
        base.StopAttack();
    }

    public override void EndAttack() {
        bossAttack.B11Base.B11Move.RestartMoveIdle();
        EndBeamLaser();
        base.EndAttack();
    }


    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float duration;
        [SerializeField] private float deltaShot;

        public float DamagePercent {
            get => damagePercent;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float Duration {
            get => duration;
        }
    }
}
