using UnityEngine;

[RequireComponent(typeof(MB06Attack), typeof(MB06Move), typeof(MB06Health))]
[RequireComponent(typeof(MB06Stat), typeof(MB06Hitbox), typeof(MB06Skill))]
[RequireComponent(typeof(MB06Effect), typeof(MB06StateController))]
public class MB06Base : MinibossBase {
    #region References
    private MB06Attack mb06Attack;
    public MB06Attack MB06Attack {
        get {
            if (mb06Attack == null) {
                mb06Attack = EnemyAttack as MB06Attack;
            }
            return mb06Attack;
        }
    }

    private MB06Move mb06Move;
    public MB06Move MB06Move {
        get {
            if (mb06Move == null) {
                mb06Move = EnemyMove as MB06Move;
            }
            return mb06Move;
        }
    }

    private MB06Health mb06Health;
    public MB06Health MB06Health {
        get {
            if (mb06Health == null) {
                mb06Health = EnemyHealth as MB06Health;
            }
            return mb06Health;
        }
    }

    private MB06Stat mb06Stat;
    public MB06Stat MB06Stat {
        get {
            if (mb06Stat == null) {
                mb06Stat = EnemyStat as MB06Stat;
            }
            return mb06Stat;
        }
    }

    private MB06Hitbox mb06Hitbox;
    public MB06Hitbox MB06Hitbox {
        get {
            if (mb06Hitbox == null) {
                mb06Hitbox = EnemyHitbox as MB06Hitbox;
            }
            return mb06Hitbox;
        }
    }

    private MB06Skill mb06Skill;
    public MB06Skill MB06Skill {
        get {
            if (mb06Skill == null) {
                mb06Skill = EnemySkill as MB06Skill;
            }
            return mb06Skill;
        }
    }
    #endregion
}
