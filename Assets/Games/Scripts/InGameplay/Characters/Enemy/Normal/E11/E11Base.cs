
using UnityEngine;

[RequireComponent(typeof(E11Attack))]
[RequireComponent(typeof(E11Move))]
[RequireComponent(typeof(E11Health))]
[RequireComponent(typeof(E11Stat))]
[RequireComponent(typeof(E11Hitbox))]
[RequireComponent(typeof(E11Skill))]
[RequireComponent(typeof(E11StateController))]
public class E11Base : EnemyBase{
    #region References
    private E11Attack e11Attack;
    public E11Attack E11Attack {
        get {
            if(e11Attack == null) {
                e11Attack = EnemyAttack as E11Attack;
            }
            return e11Attack;
        }
    }

    private E11Move e11Move;
    public E11Move E11Move {
        get {
            if(e11Move == null) {
                e11Move = EnemyMove as E11Move;
            }
            return e11Move;
        }
    }

    private E11Health e11Health;
    public E11Health E11Health {
        get {
            if(e11Health == null) {
                e11Health = EnemyHealth as E11Health;
            }
            return e11Health;
        }
    }

    private E11Stat e11Stat;
    public E11Stat E11Stat {
        get {
            if(e11Stat == null) {
                e11Stat = EnemyStat as E11Stat;
            }
            return e11Stat;
        }
    }

    private E11Hitbox e11Hitbox;
    public E11Hitbox E11Hitbox {
        get {
            if(e11Hitbox == null) {
                e11Hitbox = EnemyHitbox as E11Hitbox;
            }
            return e11Hitbox;
        }
    }

    private E11Skill e11Skill;
    public E11Skill E11Skill {
        get {
            if(e11Skill == null) {
                e11Skill = EnemySkill as E11Skill;
            }
            return e11Skill;
        }
    }

    #endregion
}
