using Helper;
using UnityEngine;

public class BossAttack : EnemyAttack {
    private BossBase bossBase;
    public BossBase BossBase {
        get {
            if (bossBase == null) {
                bossBase = EnemyBase as BossBase;
            }
            return bossBase;
        }
    }

    [SerializeField] protected BossAttackComponent[] skillAttacks;
    [SerializeField] private BossAttackComponent rageAttack;

    private BossAttackComponent currentAttack;
    protected BossAttackComponent preAttack;

    public override void PreloadIngame() {
        foreach (var s in skillAttacks) {
            s.PreloadIngame();
        }
        if (rageAttack) {
            rageAttack.PreloadIngame();
        }
    }

    public override bool CanAttack() {
        return true;
    }
    public override void Initialize() {
        base.Initialize();
        isAttacking = false;
        currentAttack = null;
        foreach (var skill in skillAttacks) {
            skill.Initialize();
        }
        if (rageAttack) {
            rageAttack.Initialize();
        }
    }

    public override void Destroy() {
        base.Destroy();
        if (currentAttack) {
            currentAttack.BossDestroy();
            currentAttack.StopAttack();
            currentAttack.EndAttack();
            currentAttack = null;
        }
        for (int i = 0; i < skillAttacks.Length; i++) {
            if (skillAttacks[i] != null) {
                skillAttacks[i].BossDestroy();
            }
        }
        if (rageAttack != null) {
            rageAttack.BossDestroy();
        }
    }

    public override void Updating() {
        base.Updating();
        if (currentAttack) {
            currentAttack.Updating();
        }
    }

    protected override void Attacking() {
        currentAttack.Attacking();
    }

    public override void EndAttack() {
        base.EndAttack();
        currentAttack = null;
    }

    public virtual void ChooseAttack() {
        BossAttackComponent randomAttack = null;
        if (skillAttacks.Length == 1) {
            randomAttack = skillAttacks[0];
        }
        else {
            do {
                randomAttack = RandomHelper.RandomInCollection(skillAttacks);
            }
            while (randomAttack == preAttack);
        }
        SetCurrentAttack(randomAttack);
    }

    protected void SetCurrentAttack(BossAttackComponent attackComponent) {
        currentAttack = attackComponent;
        preAttack = attackComponent;
        currentAttack.StartAttack();
    }

    public void StartRage() {
        SetCurrentAttack(rageAttack);
        BossBase.BossHitbox.TurnOnInvulnerable(-1);
        bossBase.BossHitbox.TurnOnShield();
    }

    public void EndRage() {
        if (preAttack == rageAttack) {
            preAttack.EndAttack();
            BossBase.IsInRageStatus = false;
            currentAttack = null;
        }
        BossBase.BossHitbox.TurnOffInvulnerable();
        bossBase.BossHitbox.TurnOffShield();
    }

    public void StopAttack() {
        if (currentAttack) {
            currentAttack.StopAttack();
            currentAttack = null;
        }
    }
#if UNITY_EDITOR
    [SerializeField] BossAttack reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        skillAttacks = reference.skillAttacks;
        rageAttack = reference.rageAttack;
    }
#endif
}
