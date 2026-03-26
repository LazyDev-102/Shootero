

using System;
using UnityEngine;

public class ME03B08Base : EnemyBase {
    #region References
    private ME03B08Attack me03B08Attack;
    public ME03B08Attack ME03B08Attack {
        get {
            if (me03B08Attack == null) {
                me03B08Attack = EnemyAttack as ME03B08Attack;
            }
            return me03B08Attack;
        }
    }

    private ME03B08Move me03B08Move;
    public ME03B08Move ME03B08Move {
        get {
            if (me03B08Move == null) {
                me03B08Move = EnemyMove as ME03B08Move;
            }
            return me03B08Move;
        }
    }

    private ME03B08Health me03B08Health;
    public ME03B08Health ME03B08Health {
        get {
            if (me03B08Health == null) {
                me03B08Health = EnemyHealth as ME03B08Health;
            }
            return me03B08Health;
        }
    }

    private ME03B08Stat me03B08Stat;
    public ME03B08Stat ME03B08Stat {
        get {
            if (me03B08Stat == null) {
                me03B08Stat = EnemyStat as ME03B08Stat;
            }
            return me03B08Stat;
        }
    }

    private ME03B08Hitbox me03B08Hitbox;
    public ME03B08Hitbox ME03B08Hitbox {
        get {
            if (me03B08Hitbox == null) {
                me03B08Hitbox = EnemyHitbox as ME03B08Hitbox;
            }
            return me03B08Hitbox;
        }
    }

    private ME03B08Skill me03B08Skill;
    public ME03B08Skill ME03B08Skill {
        get {
            if (me03B08Skill == null) {
                me03B08Skill = EnemySkill as ME03B08Skill;
            }
            return me03B08Skill;
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
        ME03B08Stat.MaxHP.SetBaseValue(hp, true);
        ME03B08Stat.Atk.SetBaseValue(atk, true);
    }
}
