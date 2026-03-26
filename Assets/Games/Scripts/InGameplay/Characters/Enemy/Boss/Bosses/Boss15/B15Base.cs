

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(B15Attack), typeof(B15Health), typeof(B15Move))]
[RequireComponent(typeof(B15Skill), typeof(B15Stat), typeof(B15Hitbox))]
[RequireComponent(typeof(B15StateController), typeof(B15Effect))]
public class B15Base : BossBase {
    #region References
    private B15Attack b15Attack;
    public B15Attack B15Attack {
        get {
            if (b15Attack == null) {
                b15Attack = BossAttack as B15Attack;
            }
            return b15Attack;
        }
    }

    private B15Move b15Move;
    public B15Move B15Move {
        get {
            if (b15Move == null) {
                b15Move = BossMove as B15Move;
            }
            return b15Move;
        }
    }

    private B15Health b15Health;
    public B15Health B15Health {
        get {
            if (b15Health == null) {
                b15Health = BossHealth as B15Health;
            }
            return b15Health;
        }
    }

    private B15Stat b15Stat;
    public B15Stat B15Stat {
        get {
            if (b15Stat == null) {
                b15Stat = BossStat as B15Stat;
            }
            return b15Stat;
        }
    }

    private B15Hitbox b15Hitbox;
    public B15Hitbox B15Hitbox {
        get {
            if (b15Hitbox == null) {
                b15Hitbox = BossHitbox as B15Hitbox;
            }
            return b15Hitbox;
        }
    }

    private B15Skill b15Skill;
    public B15Skill B15Skill {
        get {
            if (b15Skill == null) {
                b15Skill = BossSkill as B15Skill;
            }
            return b15Skill;
        }
    }
    #endregion
    private List<B15WallShield> b15WallShields = new List<B15WallShield>();
    public override void Initialize() {
        base.Initialize();
        b15WallShields.Clear();
        transform.localScale = Vector3.one * 1.5f;
    }
    public override void EndRage() {
        Gemmob.EventDispatcher.Instance.Dispatch<EventKey.OnBossRage>(new EventKey.OnBossRage() {
            bossBase = this,
            isStart = false
        });
    }
    public override void Die() {
        base.Die();
        //foreach (var item in b15WallShields) {
        //    item.DestroyImmediate();
        //}
    }
    public void AddWallShield(B15WallShield shield) {
        b15WallShields.Add(shield);
    }
}
