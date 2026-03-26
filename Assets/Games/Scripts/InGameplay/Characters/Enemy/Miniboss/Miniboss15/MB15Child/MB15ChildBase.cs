using UnityEngine;
using System;

[RequireComponent(typeof(MB15ChildAttack), typeof(MB15ChildMove), typeof(MB15ChildHealth))]
[RequireComponent(typeof(MB15ChildStat), typeof(MB15ChildHitbox), typeof(MB15ChildSkill))]
[RequireComponent(typeof(MB15ChildEffect), typeof(MB15ChildStateController))]
public class MB15ChildBase : MinibossBase {
    #region References
    private MB15ChildAttack mb15ChildAttack;
    public MB15ChildAttack MB15ChildAttack {
        get {
            if (mb15ChildAttack == null) {
                mb15ChildAttack = EnemyAttack as MB15ChildAttack;
            }
            return mb15ChildAttack;
        }
    }

    private MB15ChildMove mb15ChildMove;
    public MB15ChildMove MB15ChildMove {
        get {
            if (mb15ChildMove == null) {
                mb15ChildMove = EnemyMove as MB15ChildMove;
            }
            return mb15ChildMove;
        }
    }

    private MB15ChildHealth mb15ChildHealth;
    public MB15ChildHealth MB15ChildHealth {
        get {
            if (mb15ChildHealth == null) {
                mb15ChildHealth = EnemyHealth as MB15ChildHealth;
            }
            return mb15ChildHealth;
        }
    }

    private MB15ChildStat mb15ChildStat;
    public MB15ChildStat MB15ChildStat {
        get {
            if (mb15ChildStat == null) {
                mb15ChildStat = EnemyStat as MB15ChildStat;
            }
            return mb15ChildStat;
        }
    }

    private MB15ChildHitbox mb15ChildHitbox;
    public MB15ChildHitbox MB15ChildHitbox {
        get {
            if (mb15ChildHitbox == null) {
                mb15ChildHitbox = EnemyHitbox as MB15ChildHitbox;
            }
            return mb15ChildHitbox;
        }
    }

    private MB15ChildSkill mb15ChildSkill;
    public MB15ChildSkill MB15ChildSkill {
        get {
            if (mb15ChildSkill == null) {
                mb15ChildSkill = EnemySkill as MB15ChildSkill;
            }
            return mb15ChildSkill;
        }
    }

    #endregion

    #region  Attack
    [SerializeField] private ParticleSystem showEffect;
    [SerializeField] private ParticleSystem hideEffect;
    [SerializeField] private DotweenAnimation showAnima;
    [SerializeField] private DotweenAnimation hideAnima;

    private Action onEndBossAttack;

    private bool canMove;

    public bool CanMove {
        get {
            return canMove;
        }
    }

    public void Show() {
        canMove = false;
        if (showEffect) {
            showEffect.Play();
        }
        if (showAnima) {
            showAnima.Play(() => {
            }, true);
        }
        else {
            canMove = true;
        }

    }

    public void Hide(Action onComplete) {
        if (hideEffect) {
            hideEffect.Play();
        }
        if (hideAnima) {
            hideAnima.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    public override void Spawn() {
        Show();
    }

    public void AddOnEndBossAttack(Action onAction) {
        this.onEndBossAttack = onAction;
    }

    public void EndBossAttack() {
        onEndBossAttack?.Invoke();
        onEndBossAttack = null;
    }

    public override void Die() {
        EndBossAttack();
        base.Die();
    }

    public override void SelfDestruction() {
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
        base.SelfDestruction();
    }

    public void SetInfo(int hp, int atk) {
        MB15ChildStat.MaxHP.SetBaseValue(hp, true);
        MB15ChildStat.Atk.SetBaseValue(atk, true);
    }
    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private float shieldTimeLife = 5;

    private MB15ParentBase myParent;
    private int currentPhase;
    private bool activeShield;
    private Countdowner shieldCD = new Countdowner();

    public MB15ParentBase MyParent { get => myParent; }
    public override void Initialize() {
        canDispatchMinibossSpawn = false;
        base.Initialize();
        currentPhase = 0;
        shieldCD.StartCountdown(shieldTimeLife);
    }
    public override void Updating() {
        base.Updating();
        if (currentPhase < phases.Length && MB15ChildHealth.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            MB15ChildHitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            MB15ChildHitbox.TurnOnShield();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                MB15ChildHitbox.TurnOffShield();
            }
        }
    }
    public void SetParent(MB15ParentBase myParent) {
        this.myParent = myParent;
    }
    #endregion
}
