

using System;
using UnityEngine;
using Helper;

public class MESpecialB08Base : EnemyBase {
    #region References
    private MESpecialB08Attack meSpecialB08Attack;
    public MESpecialB08Attack MESpecialB08Attack {
        get {
            if (meSpecialB08Attack == null) {
                meSpecialB08Attack = EnemyAttack as MESpecialB08Attack;
            }
            return meSpecialB08Attack;
        }
    }

    private MESpecialB08Move meSpecialB08Move;
    public MESpecialB08Move MESpecialB08Move {
        get {
            if (meSpecialB08Move == null) {
                meSpecialB08Move = EnemyMove as MESpecialB08Move;
            }
            return meSpecialB08Move;
        }
    }

    private MESpecialB08Health meSpecialB08Health;
    public MESpecialB08Health MESpecialB08Health {
        get {
            if (meSpecialB08Health == null) {
                meSpecialB08Health = EnemyHealth as MESpecialB08Health;
            }
            return meSpecialB08Health;
        }
    }

    private MESpecialB08Stat meSpecialB08Stat;
    public MESpecialB08Stat MESpecialB08Stat {
        get {
            if (meSpecialB08Stat == null) {
                meSpecialB08Stat = EnemyStat as MESpecialB08Stat;
            }
            return meSpecialB08Stat;
        }
    }

    private MESpecialB08Hitbox meSpecialB08Hitbox;
    public MESpecialB08Hitbox MESpecialB08Hitbox {
        get {
            if (meSpecialB08Hitbox == null) {
                meSpecialB08Hitbox = EnemyHitbox as MESpecialB08Hitbox;
            }
            return meSpecialB08Hitbox;
        }
    }

    private MESpecialB08Skill meSpecialB08Skill;
    public MESpecialB08Skill MESpecialB08Skill {
        get {
            if (meSpecialB08Skill == null) {
                meSpecialB08Skill = EnemySkill as MESpecialB08Skill;
            }
            return meSpecialB08Skill;
        }
    }

    #endregion

    [SerializeField] private ParticleSystem showEffect;
    [SerializeField] private ParticleSystem hideEffect;
    [SerializeField] private DotweenAnimation showAnima;
    [SerializeField] private DotweenAnimation hideAnima;

    private bool isMoveToTarget;


    private Action<MESpecialB08Base> onMEDead;

    private bool isDie;

    private Vector2 localPosition;
    private float localEuler;


    public bool IsMoveToTarget { get => isMoveToTarget; }
    public new bool IsDie { get => isDie; }

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
        Show();
    }
    public override void SelfDestruction() {
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
        base.SelfDestruction();
    }

    public void SetLocalPosition(Vector2 position) {
        localPosition = position;
        transform.localPosition = position;
    }

    public void SetLocalEuler(float euler) {
        localEuler = euler;
        transform.RotateLocalEuler(euler);
    }

    public void ResetLocal() {
        transform.localPosition = localPosition;
        transform.RotateLocalEuler(localEuler);
    }

    public override void Initialize() {
        base.Initialize();
        isMoveToTarget = false;
        isDie = false;
    }

    public void StartMoveTarget() {
        isMoveToTarget = true;
        MESpecialB08Move.StartMoveTarget();
    }

    public void SetInfo(int hp, int atk) {
        EnemyStat.MaxHP.SetBaseValue(hp, true);
        EnemyStat.Atk.SetBaseValue(atk, true);
    }

    public override void Die() {
        MESpecialB08Attack.Attack();
        gameObject.SetActive(false);
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
        onMEDead?.Invoke(this);
        onMEDead = null;
        isDie = true;
    }

    public void ForceDie() {
        isDie = true;
        gameObject.SetActive(false);
    }

    public void AddOnMEDead(Action<MESpecialB08Base> action) {
        onMEDead = action;
    }
}

