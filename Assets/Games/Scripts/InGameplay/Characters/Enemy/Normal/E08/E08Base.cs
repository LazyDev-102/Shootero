
using UnityEngine;

[RequireComponent(typeof(E08Attack))]
[RequireComponent(typeof(E08Move))]
[RequireComponent(typeof(E08Health))]
[RequireComponent(typeof(E08Stat))]
[RequireComponent(typeof(E08Hitbox))]
[RequireComponent(typeof(E08Skill))]
[RequireComponent(typeof(E08StateController))]
public class E08Base : EnemyBase {
    #region References
    private E08Attack e08Attack;
    public E08Attack E08Attack {
        get {
            if(e08Attack == null) {
                e08Attack = EnemyAttack as E08Attack;
            }
            return e08Attack;
        }
    }

    private E08Move e08Move;
    public E08Move E08Move {
        get {
            if(e08Move == null) {
                e08Move = EnemyMove as E08Move;
            }
            return e08Move;
        }
    }

    private E08Health e08Health;
    public E08Health E08Health {
        get {
            if(e08Health == null) {
                e08Health = EnemyHealth as E08Health;
            }
            return e08Health;
        }
    }

    private E08Stat e08Stat;
    public E08Stat E08Stat {
        get {
            if(e08Stat == null) {
                e08Stat = EnemyStat as E08Stat;
            }
            return e08Stat;
        }
    }

    private E08Hitbox e08Hitbox;
    public E08Hitbox E08Hitbox {
        get {
            if(e08Hitbox == null) {
                e08Hitbox = EnemyHitbox as E08Hitbox;
            }
            return e08Hitbox;
        }
    }

    private E08Skill e08Skill;
    public E08Skill E08Skill {
        get {
            if(e08Skill == null) {
                e08Skill = EnemySkill as E08Skill;
            }
            return e08Skill;
        }
    }

    #endregion
}
