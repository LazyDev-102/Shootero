using DG.Tweening;
using UnityEngine;

public class MB02Base : MinibossBase {
    #region References
    private MB02Attack mb02Attack;
    public MB02Attack MB02Attack {
        get {
            if (mb02Attack == null) {
                mb02Attack = EnemyAttack as MB02Attack;
            }
            return mb02Attack;
        }
    }

    private MB02Move mb02Move;
    public MB02Move MB02Move {
        get {
            if (mb02Move == null) {
                mb02Move = EnemyMove as MB02Move;
            }
            return mb02Move;
        }
    }

    private MB02Health mb02Health;
    public MB02Health MB02Health {
        get {
            if (mb02Health == null) {
                mb02Health = EnemyHealth as MB02Health;
            }
            return mb02Health;
        }
    }

    private MB02Stat mb02Stat;
    public MB02Stat MB02Stat {
        get {
            if (mb02Stat == null) {
                mb02Stat = EnemyStat as MB02Stat;
            }
            return mb02Stat;
        }
    }

    private MB02Hitbox mb02Hitbox;
    public MB02Hitbox MB02Hitbox {
        get {
            if (mb02Hitbox == null) {
                mb02Hitbox = EnemyHitbox as MB02Hitbox;
            }
            return mb02Hitbox;
        }
    }

    private MB02Skill mb02Skill;
    public MB02Skill MB02Skill {
        get {
            if (mb02Skill == null) {
                mb02Skill = EnemySkill as MB02Skill;
            }
            return mb02Skill;
        }
    }
    #endregion

    #region Tutorial
    public override void Die() {
        base.Die();
        if (GameResources.Instance.ConquerorData.IsTut) {
            GameManager.Instance.GameLoader.DespawnAllEnemy(true);
            var p = IngameHUD.Instance.GetCombat<ConquerorCombatPanel>();
            if (p != null) {
                p.HideAllUI();
                DOVirtual.DelayedCall(3f, () => p.Spawn4ngel()).SetUpdate(true);
            }
        }
    }
    #endregion
}
