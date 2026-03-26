
using UnityEngine;

public abstract class EnemyAttack : CharacterAttack {
    private EnemyBase enemyBase;
    public EnemyBase EnemyBase {
        get {
            if (enemyBase == null) {
                enemyBase = CharacterBase as EnemyBase;
            }
            return enemyBase;
        }
    }

    [SerializeField] private Transform target;
    protected bool isAttacking;

    public Transform Target {
        get {
            if (target == null) {
                target = GameManager.Instance.GameLoader.Ship.transform;
                //target = FindObjectOfType<ShipBase>().transform;
            }
            return target;
        }
    }

    public abstract bool CanAttack();

    public void Attack() {
        StartAttack();
        Attacking();
    }

    protected virtual void StartAttack() {
        isAttacking = true;
    }
    protected abstract void Attacking();
    public virtual void EndAttack() {
        isAttacking = false;
    }
    public bool IsAttacking() {
        return isAttacking;
    }

    protected virtual U ChangingBullet<U>(U bullet) where U : BulletBase {
        bullet.SetHitInfor(EnemyBase.EnemyStat.Atk.Value, null, EnemyBase);
        //bullet.SetSize(EnemyBase.EnemyStat.Size.Value);
        return bullet;
    }
}
