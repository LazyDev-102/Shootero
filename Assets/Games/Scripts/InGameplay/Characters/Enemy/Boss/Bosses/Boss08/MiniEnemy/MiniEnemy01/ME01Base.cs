

using System;
using UnityEngine;

public class ME01Base : EnemyBase {
    #region References
    private ME01Attack me01Attack;
    public ME01Attack ME01Attack {
        get {
            if (me01Attack == null) {
                me01Attack = EnemyAttack as ME01Attack;
            }
            return me01Attack;
        }
    }

    private ME01Move me01Move;
    public ME01Move ME01Move {
        get {
            if (me01Move == null) {
                me01Move = EnemyMove as ME01Move;
            }
            return me01Move;
        }
    }

    private ME01Health me01Health;
    public ME01Health ME01Health {
        get {
            if (me01Health == null) {
                me01Health = EnemyHealth as ME01Health;
            }
            return me01Health;
        }
    }

    private ME01Stat me01Stat;
    public ME01Stat ME01Stat {
        get {
            if (me01Stat == null) {
                me01Stat = EnemyStat as ME01Stat;
            }
            return me01Stat;
        }
    }

    private ME01Hitbox me01Hitbox;
    public ME01Hitbox ME01Hitbox {
        get {
            if (me01Hitbox == null) {
                me01Hitbox = EnemyHitbox as ME01Hitbox;
            }
            return me01Hitbox;
        }
    }

    private ME01Skill me01Skill;
    public ME01Skill ME01Skill {
        get {
            if (me01Skill == null) {
                me01Skill = EnemySkill as ME01Skill;
            }
            return me01Skill;
        }
    }
    #endregion

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
                canMove = true;
            }, true);
        }
    }

    public void Hide(Action onComplete) {
        if (hideEffect) {
            hideEffect.Play();
        }
        if (hideAnima) {
            hideAnima.Play(onComplete, true);
        }
    }

    public override void Spawn() {
        Vector2 positionSpawn = ME01Move.GetPointSpawn();
        transform.position = positionSpawn;
        Show();
    }

    public override void Initialize() {
        base.Initialize();
        canMove = false;
        if (showAnima) {
            showAnima.Initialize();
        }
        if (hideAnima) {
            hideAnima.Initialize();
        }
    }

    public override void SelfDestruction() {
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
        base.SelfDestruction();
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

    public void SetInfo(int hp, int atk) {
        ME01Stat.Atk.SetBaseValue(atk, true);
        ME01Stat.MaxHP.SetBaseValue(hp, true);
    }
}
