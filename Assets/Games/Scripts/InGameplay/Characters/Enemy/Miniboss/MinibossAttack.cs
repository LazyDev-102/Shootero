using Helper;
using UnityEngine;

public class MinibossAttack : EnemyAttack {
    private MinibossBase minibossBase;
    public MinibossBase MinibossBase {
        get {
            if (minibossBase == null) {
                minibossBase = EnemyBase as MinibossBase;
            }
            return minibossBase;
        }
    }

    [SerializeField] protected MinibossAttackComponent[] skillAttacks;
    [SerializeField] protected MinibossAttackComponent specialAttack;

    protected MinibossAttackComponent currentAttack;
    private MinibossAttackComponent preAttack;


    public override void PreloadIngame() {
        foreach (var s in skillAttacks) {
            s.PreloadIngame();
        }
        if (specialAttack) {
            specialAttack.PreloadIngame();
        }
    }

    public override bool CanAttack() {
        return true;
    }
    public override void Initialize() {
        base.Initialize();
        foreach (var skill in skillAttacks) {
            skill.Initialize();
        }
        if (specialAttack) {
            specialAttack.Initialize();
        }
    }

    public override void Destroy() {
        base.Destroy();
        if (currentAttack) {
            currentAttack.StopAttack();
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
        if (specialAttack != null && currentAttack == specialAttack)
            EndSpecialAttack();

        currentAttack = null;
    }

    public void ChooseAttack() {
        if (currentAttack != null && currentAttack == specialAttack)
            return;
        MinibossAttackComponent randomAttack = null;
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

    protected void SetCurrentAttack(MinibossAttackComponent attackComponent) {
        currentAttack = attackComponent;
        preAttack = attackComponent;
        currentAttack.StartAttack();
    }

    public void StopAttack() {
        if (currentAttack) {
            currentAttack.StopAttack();
            currentAttack = null;
        }
    }

    public void StartSpecialAttack() {
        if (specialAttack != null) {
            SetCurrentAttack(specialAttack);
        }
    }

    public void EndSpecialAttack() {
        MinibossBase.IsSpecialState = false;
    }
#if UNITY_EDITOR
    [SerializeField] MinibossAttack reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        skillAttacks = reference.skillAttacks;
        specialAttack = reference.specialAttack;
    }
#endif
}
