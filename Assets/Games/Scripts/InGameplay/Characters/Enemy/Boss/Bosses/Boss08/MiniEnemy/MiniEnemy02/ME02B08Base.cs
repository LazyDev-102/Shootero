
using System;
using UnityEngine;

public class ME02B08Base : EnemyBase {
    #region References
    private ME02B08Attack me02B08Attack;
    public ME02B08Attack ME02B08Attack {
        get {
            if (me02B08Attack == null) {
                me02B08Attack = EnemyAttack as ME02B08Attack;
            }
            return me02B08Attack;
        }
    }

    private ME02B08Move me02B08Move;
    public ME02B08Move ME02B08Move {
        get {
            if (me02B08Move == null) {
                me02B08Move = EnemyMove as ME02B08Move;
            }
            return me02B08Move;
        }
    }

    private ME02B08Health me02B08Health;
    public ME02B08Health ME02B08Health {
        get {
            if (me02B08Health == null) {
                me02B08Health = EnemyHealth as ME02B08Health;
            }
            return me02B08Health;
        }
    }

    private ME02B08Stat me02B08Stat;
    public ME02B08Stat ME02B08Stat {
        get {
            if (me02B08Stat == null) {
                me02B08Stat = EnemyStat as ME02B08Stat;
            }
            return me02B08Stat;
        }
    }

    private ME02B08Hitbox me02B08Hitbox;
    public ME02B08Hitbox ME02B08Hitbox {
        get {
            if (me02B08Hitbox == null) {
                me02B08Hitbox = EnemyHitbox as ME02B08Hitbox;
            }
            return me02B08Hitbox;
        }
    }

    private ME02B08Skill me02B08Skill;
    public ME02B08Skill ME02B08Skill {
        get {
            if (me02B08Skill == null) {
                me02B08Skill = EnemySkill as ME02B08Skill;
            }
            return me02B08Skill;
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
        ME02B08Stat.MaxHP.SetBaseValue(hp, true);
        ME02B08Stat.Atk.SetBaseValue(atk, true);
    }
}
