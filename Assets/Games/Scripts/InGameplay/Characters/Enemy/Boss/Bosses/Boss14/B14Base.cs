using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(B14Attack), typeof(B14Health), typeof(B14Move))]
[RequireComponent(typeof(B14Skill), typeof(B14Stat), typeof(B14HitBox))]
[RequireComponent(typeof(B14StateController), typeof(B14Effect))]
public class B14Base : BossBase {
    #region References
    private B14Attack b14Attack;
    public B14Attack B14Attack {
        get {
            if (b14Attack == null) {
                b14Attack = BossAttack as B14Attack;
            }
            return b14Attack;
        }
    }

    private B14Move b14Move;
    public B14Move B14Move {
        get {
            if (b14Move == null) {
                b14Move = BossMove as B14Move;
            }
            return b14Move;
        }
    }

    private B14Health b14Health;
    public B14Health B14Health {
        get {
            if (b14Health == null) {
                b14Health = BossHealth as B14Health;
            }
            return b14Health;
        }
    }

    private B14Stat b14Stat;
    public B14Stat B14Stat {
        get {
            if (b14Stat == null) {
                b14Stat = BossStat as B14Stat;
            }
            return b14Stat;
        }
    }

    private B14HitBox b14Hitbox;
    public B14HitBox B14Hitbox {
        get {
            if (b14Hitbox == null) {
                b14Hitbox = BossHitbox as B14HitBox;
            }
            return b14Hitbox;
        }
    }

    private B14Skill b14Skill;
    public B14Skill B14Skill {
        get {
            if (b14Skill == null) {
                b14Skill = BossSkill as B14Skill;
            }
            return b14Skill;
        }
    }
    #endregion


    #region Attack
    [SerializeField] private List<B14Piece> b14Children = new List<B14Piece>();
    [SerializeField] private float maxHPShieldPercent;
    [SerializeField] private float attackShieldPercent;
    [SerializeField] private Collider2D shieldMainPiece;

    public override void Spawn() {
        base.Spawn();
        for (int i = 0; i < b14Children.Count; ++i) {
            b14Children[i].Initialize();
            b14Children[i].SetParent(this)
                          .SetMaxHeath(B14Stat.MaxHP.Value / (b14Children.Count + 2))
                          .OnHitDame(UpdateHealth);
        }
    }
    public override void Initialize() {
        base.Initialize();
        B14Hitbox.TurnOnInvulnerable(-1);
        shieldMainPiece.enabled = true;
    }
    private void UpdateHealth(int hp) {
        int totalHeal = 0;
        int curHeal = 0;
        foreach (var e in b14Children) {
            totalHeal += e.Hp;
            curHeal += e.CurrentHP < 0 ? 0 : e.CurrentHP;
        }
        totalHeal += B14Stat.MaxHP.Value / 5;
        curHeal += B14Stat.MaxHP.Value / 5;
        B14Health.DispatchOnHpChanged(curHeal, totalHeal);
        CheckPhase(curHeal, totalHeal);
        CheckInvulnerable();
    }
    private void CheckInvulnerable() {
        for (int i = 0; i < b14Children.Count; i++) {
            if (b14Children[i].gameObject.activeInHierarchy) {
                B14Hitbox.TurnOnInvulnerable(-1);
                return;
            }
        }
        shieldMainPiece.enabled = false;
        B14Hitbox.TurnOffInvulnerable();
    }
    public bool CanHitDamage() {
        return shieldMainPiece.enabled == false;
    }
    public void PierceCanHitDamage(bool status) {
        foreach (var item in b14Children) {
            item.ChangeCanHitDamage(status);
        }
    }
    public override void ChangeToNextPhase() {
        base.ChangeToNextPhase();
        PierceCanHitDamage(false);
    }
    #endregion
}
