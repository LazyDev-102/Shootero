using UnityEngine;
using System;
using Gemmob;
using Helper;

public class BossBase : EnemyBase {
    #region References
    private BossAttack bossAttack;
    public BossAttack BossAttack {
        get {
            if (bossAttack == null) {
                bossAttack = EnemyAttack as BossAttack;
            }
            return bossAttack;
        }
    }

    private BossMove bossMove;
    public BossMove BossMove {
        get {
            if (bossMove == null) {
                bossMove = EnemyMove as BossMove;
            }
            return bossMove;
        }
    }

    private BossHealth bossHealth;
    public BossHealth BossHealth {
        get {
            if (bossHealth == null) {
                bossHealth = EnemyHealth as BossHealth;
            }
            return bossHealth;
        }
    }

    private BossStat bossStat;
    public BossStat BossStat {
        get {
            if (bossStat == null) {
                bossStat = EnemyStat as BossStat;
            }
            return bossStat;
        }
    }

    private BossHitbox bossHitbox;
    public BossHitbox BossHitbox {
        get {
            if (bossHitbox == null) {
                bossHitbox = EnemyHitbox as BossHitbox;
            }
            return bossHitbox;
        }
    }

    private BossSkill bossSkill;
    public BossSkill BossSkill {
        get {
            if (bossSkill == null) {
                bossSkill = EnemySkill as BossSkill;
            }
            return bossSkill;
        }
    }

    private BossEffect bossEffect;
    public BossEffect BossEffect {
        get {
            if (bossEffect == null) {
                bossEffect = EnemyEffect as BossEffect;
            }
            return bossEffect;
        }
    }
    #endregion
    [Header("Boss")]
    [SerializeField] private int bossIndex;
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private float idleTimeAfterAppear;
    [SerializeField] private float idleTimeAfterAttack;


    private int currentPhaseIndex;
    private bool isInRageStatus;
    private Countdowner idleCountdowner;
    private bool isInEffectRage;
    private int preHp;
    private bool isIdleAfterAppear;


    public int BossIndex {
        get {
            return bossIndex;
        }
    }

    public PhaseData CurrentPhaseData {
        get {
            return phases[currentPhaseIndex];
        }
    }


    public int CurrentPhaseIndex {
        get {
            return currentPhaseIndex;
        }
    }

    public bool IsMaxPhase {
        get {
            return currentPhaseIndex == phases.Length - 1;
        }
    }

    public bool IsInRageStatus { get => isInRageStatus; set => isInRageStatus = value; }
    public bool IsInEffectRage { get => isInEffectRage; set => isInEffectRage = value; }


    public override void Initialize() {
        base.Initialize();
        currentPhaseIndex = 0;
        isInRageStatus = false;
        isInEffectRage = false;
        isIdleAfterAppear = false;
        EventDispatcher.Instance.Dispatch(new EventKey.OnBossSpawnParam() {
            bossBase = this,
            isSpawn = true
        });
        BossHitbox.TurnOnShield();
    }

    public override void Destroy() {
        base.Destroy();
        EventDispatcher.Instance.Dispatch(new EventKey.OnBossSpawnParam() {
            bossBase = this,
            isSpawn = false
        });
    }

    public override void Die() {
        BossMove.EndMove();
        BossAttack.StopAttack();
        BossEffect.StartPreDieBoss(() => {
            ObjectBase lastCauser = CharacterHitbox.LastCauser;
            if (lastCauser) {
                lastCauser.Killing(this);
            }
            foreach (var assister in CharacterHitbox.AssisCausers) {
                assister.Assising(this);
            }
            if (explosion) {
                GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
            }
            CameraShakeManager.Instance.ShakeCamera(shakeType);

            if (enableDropChip && !GameManager.Instance.isTest) {
                GameResources.Instance.Drop.Droping(transform.position, this);
                if (canDropChip) {
                    GameResources.Instance.Drop.DropingChip(transform.position, this);
                    canDropChip = false;
                }
            }
            if (!GameManager.Instance.isTest) {
                SoundManager.Instance.PlayBossDestroy();
            }
            onDie?.Invoke();
            RemoveAllOnDie();
            RemoveMe();
        });
        DispatchOnDie();
    }
    protected override void RemoveMe() {
        GameManager.Instance.GameLoader.DespawnEnemy(this);
        onRemove?.Invoke(this);
    }
    protected override void DispatchOnDie() {
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.DefeatBoss, 1);
        EventDispatcher.Instance.Dispatch(EventKey.OnDefeatBoss);
        if (GameManager.Instance.GameMode == GameMode.EventHalloween) {
            EventDispatcher.Instance.Dispatch(EventKey.HalloweenDefeatBoss);
        }
        if (GameManager.Instance.GameMode == GameMode.EventXmas) {
            EventDispatcher.Instance.Dispatch(EventKey.XmasDefeatBoss);
        }
    }
    public virtual void StartRage() {
        isInEffectRage = true;
        BossMove.RageKnockback();
        BossEffect.StartBreakEffect();
        BossHitbox.TurnOnInvulnerable(-1);
        BossAttack.StopAttack();
        BossHitbox.TurnOnShield();
        EventDispatcher.Instance.Dispatch<EventKey.OnBossRage>(new EventKey.OnBossRage() {
            bossBase = this,
            isStart = true
        });


    }

    public virtual void EndRage() {
        BossHitbox.TurnOffShield();
        BossHitbox.TurnOffInvulnerable();
        EventDispatcher.Instance.Dispatch<EventKey.OnBossRage>(new EventKey.OnBossRage() {
            bossBase = this,
            isStart = false
        });
    }

    public virtual void CheckPhase() {
        if (IsMaxPhase) {
            return;
        }
        int currentHp = BossHealth.CurrentHp;
        if (currentHp == preHp) {
            return;
        }
        preHp = currentHp;
        int maxHp = BossStat.MaxHP.Value;
        float currentHpPercent = currentHp * 100.0f / maxHp;
        float nextHpPercentMilestone = phases[currentPhaseIndex + 1].HpPercentMilestone;
        if (currentHpPercent <= nextHpPercentMilestone) {
            ChangeToNextPhase();
        }
    }
    public virtual void CheckPhase(int currentHp, int maxHp) {
        if (IsMaxPhase) {
            return;
        }
        if (currentHp == preHp) {
            return;
        }
        preHp = currentHp;
        float currentHpPercent = currentHp * 100.0f / maxHp;
        float nextHpPercentMilestone = phases[currentPhaseIndex + 1].HpPercentMilestone;
        if (currentHpPercent <= nextHpPercentMilestone) {
            ChangeToNextPhase();
        }
    }

    public virtual void ChangeToNextPhase() {
        currentPhaseIndex++;
        isInRageStatus = true;
    }

    public void StartIdleAfterAppear() {
        idleCountdowner.StartCountdown(idleTimeAfterAppear);
        BossMove.StartMoveIdle();
        isIdleAfterAppear = true;
    }

    public void StartIdleAfterAttack() {
        idleCountdowner.StartCountdown(idleTimeAfterAttack);
        BossMove.StartMoveIdle();
    }

    public void CountdownIdle() {
        idleCountdowner.Countdowning(Time.deltaTime);
    }

    public bool IsEndIdle() {
        return idleCountdowner.IsTimeOut();
    }

    public void EndIdle() {
        if (isIdleAfterAppear) {
            isIdleAfterAppear = false;
            BossHitbox.TurnOffShield();
            GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
        }
    }
#if UNITY_EDITOR
    [SerializeField] BossBase reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        explosion = reference.explosion;
        numberExplosion = reference.numberExplosion;
        deltaExplosion = reference.deltaExplosion;
        radiusExplosion = reference.radiusExplosion;
        shakeType = reference.shakeType;
        type = reference.type;
        spawnBorderOffset = reference.spawnBorderOffset;
        spawnBorderType = reference.spawnBorderType;
        enableDropChip = reference.enableDropChip;
        score = reference.score;
        bossIndex = reference.bossIndex;
        phases = reference.phases;
        idleTimeAfterAppear = reference.idleTimeAfterAppear;
        idleTimeAfterAttack = reference.idleTimeAfterAttack;
    }
#endif
}


[Serializable]
public class PhaseData {
    [SerializeField] private float hpPercentMilestone;

    public float HpPercentMilestone { get => hpPercentMilestone; }
}