using Helper;
using UnityEngine;

public class B03RageAttackComponent : BossAttackComponent {
    [SerializeField] private B03Attack bossAttack;
    [SerializeField] private bool enableMove;
    [SerializeField] private bool minDistance;
    [SerializeField] private Area moveArea;

    private bool isShieldMoveOut;
    private B03Base b03Base;
    private bool isStarted;
    public override void Initialize() {
        base.Initialize();
        isStarted = false;
    }
    public override void Attacking() {
        this.DelayWait(0.1f, () => {
            b03Base.StartShieldMoveOut();
            isShieldMoveOut = true;
            isStarted = true;
        });

    }

    public override void StartAttack() {
        b03Base = bossAttack.B03Base;
        isStarted = false;
    }

    public override void Updating() {
        if (isStarted) {
            b03Base.StartRageRotateShield();
            b03Base.UpdateShieldMove();
            if (b03Base.IsShieldCompletedMove()) {
                if (isShieldMoveOut) {
                    isShieldMoveOut = false;
                    b03Base.StartShieldMoveIn();
                }
                else {
                    isShieldMoveOut = true;
                    b03Base.StartShieldMoveOut();
                }
            }
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

}
