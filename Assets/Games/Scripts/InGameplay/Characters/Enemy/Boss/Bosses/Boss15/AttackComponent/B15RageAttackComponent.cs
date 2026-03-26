

using DG.Tweening;
using Gemmob;
using System.Collections.Generic;
using UnityEngine;

public class B15RageAttackComponent : BossAttackComponent {
    [SerializeField] private B15Attack bossAttack;
    [SerializeField] private B15WallShield wallShield;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private List<B15WallShield> b15Walls = new List<B15WallShield>();
    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B15Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B15Base.CurrentPhaseIndex];
        }
    }

    public override void Attacking() {
        var wall = wallShield.Spawn(CommonHUD.Instance.transform, Vector3.zero);
        wall.transform.localPosition = Vector3.zero;
        wall.transform.position = Vector3.right * 15;
        wall.EnableWallShield((int)(bossAttack.B15Base.B15Stat.MaxHP.Value * attackData.ShieldHpPercent), (int)(bossAttack.B15Base.B15Stat.Atk.Value * attackData.ShieldAttackPercent), bossAttack.B15Base.CurrentPhaseIndex - 1);
        wall.SetActionOnDie();
        bossAttack.B15Base.AddWallShield(wall);
        b15Walls.Add(wall);
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        DOVirtual.DelayedCall(2f, EndAttack);
        bossAttack.B15Base.B15Hitbox.TurnOnInvulnerable(5);
        bossAttack.B15Base.B15Hitbox.TurnOnShield();
        DOVirtual.DelayedCall(5f, () => {
            bossAttack.B15Base.B15Hitbox.TurnOffInvulnerable();
            bossAttack.B15Base.B15Hitbox.TurnOffShield();
        });
    }

    public override void Updating() {

    }
    public override void EndAttack() {
        base.EndAttack();
        bossAttack.BossBase.IsInRageStatus = false;
        bossAttack.B15Base.B15Hitbox.TurnOnInvulnerable(5);
    }
    public override void BossDestroy() {
        base.BossDestroy();
        for (int i = 0; i < b15Walls.Count; i++) {
            b15Walls[i].Recycle();
        }
        b15Walls.Clear();
    }
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float shieldHpPercent;
        [SerializeField] private float shieldAttackPercent;

        public float ShieldHpPercent { get => shieldHpPercent; }
        public float ShieldAttackPercent { get => shieldAttackPercent; }
    }
}
