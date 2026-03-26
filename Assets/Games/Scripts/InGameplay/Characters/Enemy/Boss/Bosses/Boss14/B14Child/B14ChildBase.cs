using UnityEngine;
using System;

[RequireComponent(typeof(B14ChildAttack), typeof(B14ChildMove), typeof(B14ChildHealth))]
[RequireComponent(typeof(B14ChildStat), typeof(B14ChildHitbox), typeof(B14ChildSkill))]
[RequireComponent(typeof(B14ChildEffect), typeof(B14ChildStateController))]
public class B14ChildBase : BossBase {
    #region References
    private B14ChildAttack mb15ChildAttack;
    public B14ChildAttack B14ChildAttack {
        get {
            if (mb15ChildAttack == null) {
                mb15ChildAttack = EnemyAttack as B14ChildAttack;
            }
            return mb15ChildAttack;
        }
    }

    private B14ChildMove mb15ChildMove;
    public B14ChildMove B14ChildMove {
        get {
            if (mb15ChildMove == null) {
                mb15ChildMove = EnemyMove as B14ChildMove;
            }
            return mb15ChildMove;
        }
    }

    private B14ChildHealth mb15ChildHealth;
    public B14ChildHealth B14ChildHealth {
        get {
            if (mb15ChildHealth == null) {
                mb15ChildHealth = EnemyHealth as B14ChildHealth;
            }
            return mb15ChildHealth;
        }
    }

    private B14ChildStat mb15ChildStat;
    public B14ChildStat B14ChildStat {
        get {
            if (mb15ChildStat == null) {
                mb15ChildStat = EnemyStat as B14ChildStat;
            }
            return mb15ChildStat;
        }
    }

    private B14ChildHitbox mb15ChildHitbox;
    public B14ChildHitbox B14ChildHitbox {
        get {
            if (mb15ChildHitbox == null) {
                mb15ChildHitbox = EnemyHitbox as B14ChildHitbox;
            }
            return mb15ChildHitbox;
        }
    }

    private B14ChildSkill mb15ChildSkill;
    public B14ChildSkill B14ChildSkill {
        get {
            if (mb15ChildSkill == null) {
                mb15ChildSkill = EnemySkill as B14ChildSkill;
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
        B14ChildStat.MaxHP.SetBaseValue(hp, true);
        B14ChildStat.Atk.SetBaseValue(atk, true);
    }
    #endregion

    #region Special Attack
    [SerializeField] private PhaseData[] phases;
    [SerializeField] private float shieldTimeLife = 5;

    private B14Base myParent;
    private int currentPhase;
    private bool activeShield;
    private Countdowner shieldCD = new Countdowner();

    public B14Base MyParent { get => myParent; }
    public override void Initialize() {
        base.Initialize();
        currentPhase = 0;
        shieldCD.StartCountdown(shieldTimeLife);
    }
    public override void Updating() {
        base.Updating();
        if (currentPhase < phases.Length && B14ChildHealth.GetPercentHPRemain() < phases[currentPhase].HpPercentMilestone) {
            currentPhase++;
            B14ChildHitbox.TurnOnInvulnerable(shieldTimeLife + 1);
            B14ChildHitbox.TurnOnShield();
            activeShield = true;
            shieldCD.StartCountdown(shieldTimeLife);
        }

        if (activeShield) {
            if (shieldCD.IsCountdowning()) {
                shieldCD.Countdowning(Time.deltaTime);
            }
            else {
                activeShield = false;
                B14ChildHitbox.TurnOffShield();
            }
        }
    }
    public void SetParent(B14Base myParent) {
        this.myParent = myParent;
    }
    #endregion
}
