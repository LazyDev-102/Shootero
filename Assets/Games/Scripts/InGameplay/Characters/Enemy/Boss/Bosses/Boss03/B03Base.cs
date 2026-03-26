


using Gemmob;
using Helper;
using UnityEngine;

public class B03Base : BossBase {
    #region References
    private B03Attack b03Attack;
    public B03Attack B03Attack {
        get {
            if (b03Attack == null) {
                b03Attack = BossAttack as B03Attack;
            }
            return b03Attack;
        }
    }

    private B03Move b03Move;
    public B03Move B03Move {
        get {
            if (b03Move == null) {
                b03Move = BossMove as B03Move;
            }
            return b03Move;
        }
    }

    private B03Health b03Health;
    public B03Health B03Health {
        get {
            if (b03Health == null) {
                b03Health = BossHealth as B03Health;
            }
            return b03Health;
        }
    }

    private B03Stat b03Stat;
    public B03Stat B03Stat {
        get {
            if (b03Stat == null) {
                b03Stat = BossStat as B03Stat;
            }
            return b03Stat;
        }
    }

    private B03Hitbox b03Hitbox;
    public B03Hitbox B03Hitbox {
        get {
            if (b03Hitbox == null) {
                b03Hitbox = BossHitbox as B03Hitbox;
            }
            return b03Hitbox;
        }
    }

    private B03Skill b03Skill;
    public B03Skill B03Skill {
        get {
            if (b03Skill == null) {
                b03Skill = BossSkill as B03Skill;
            }
            return b03Skill;
        }
    }
    #endregion
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private MiniShieldBase[] shields;
    [SerializeField] private float timeStagger;
    [SerializeField] private float delayMoveRage;

    [SerializeField] private Transform shieldContainer;
    [SerializeField] private float normalRotateShield;
    [SerializeField] private float rageRotateShield;
    [SerializeField] private float maxHPShieldPercent;
    [SerializeField] private float attackShieldPercent;
    [SerializeField] private float attackLightPercent;


    private Countdowner delayMoveRageCountdowner = new Countdowner();
    private Countdowner staggerCountdowner = new Countdowner();
    private float currentRotateShield;

    public override void Initialize() {
        base.Initialize();
        GameObject shield = shieldPrefab.Spawn(transform.position, false);
        shieldContainer = shield.transform;
        shields = shield.GetComponentsInChildren<MiniShieldBase>();
        foreach (var s in shields) {
            s.MiniShieldStat.Atk.SetBaseValue((int)(B03Stat.Atk.Value * attackShieldPercent), true);
            s.MiniShieldStat.MaxHP.SetBaseValue((int)(B03Stat.MaxHP.Value * maxHPShieldPercent), true);
            s.Initialize();
            s.LightningLine.SetInfor((int)(B03Stat.Atk.Value * attackLightPercent), this);
        }
        StartRageRotateShield();
    }

    public override void Destroy() {
        base.Destroy();
#if UNITY_EDITOR
        DestroyImmediate(shieldContainer.gameObject);
#else
        Destroy(shieldContainer.gameObject);
#endif
    }

    public override void Updating() {
        base.Updating();
        if (shieldContainer) {
            shieldContainer.position = BossMove.MyRigi.position;
        }
        RotatingShield();
        if (CanStagger() && staggerCountdowner.IsTimeOut()) {
            StartStagger();
        }
        if (staggerCountdowner.IsCountdowning()) {
            staggerCountdowner.Countdowning(Time.deltaTime);
            if (staggerCountdowner.IsTimeOut()) {
                RestoreAllShield();
            }
        }
    }

    public bool CanStagger() {
        if (shields == null) {
            return false;
        }
        foreach (var s in shields) {
            if (s != null && !s.IsDie()) {
                return false;
            }
        }
        return true;
    }

    public void StartStagger() {
        staggerCountdowner.StartCountdown(timeStagger);
    }

    public void UpdatingStagger() {
        staggerCountdowner.Countdowning(Time.deltaTime);
    }

    public bool IsEndStagger() {
        return staggerCountdowner.IsTimeOut();
    }

    public void RestoreAllShield() {
        B03Hitbox.TurnOffShield();

        if (shields == null) {
            return;
        }
        foreach (var s in shields) {
            s.Restore();
        }
    }

    public void RestoreAllShield1() {

        if (shields == null) {
            return;
        }
        foreach (var s in shields) {
            s.Restore();
        }
    }

    public void StartLookDown() {
        delayMoveRageCountdowner.StartCountdown(delayMoveRage);
    }

    public void LookingDown() {
        delayMoveRageCountdowner.Countdowning(Time.deltaTime);
        B03Move.LookDirection(UnityHelper.Down);
    }

    public bool CanMoveRage() {
        return delayMoveRageCountdowner.IsTimeOut();
    }

    public void StartShieldMoveOut() {
        if (shields == null) {
            return;
        }
        foreach (var s in shields) {
            s.MiniShieldMove.StartMoveOut();
        }
    }

    public void StartShieldMoveIn() {
        if (shields == null) {
            return;
        }
        foreach (var s in shields) {
            s.MiniShieldMove.StartMoveIn();
        }
    }

    public void UpdateShieldMove() {
        if (shields == null) {
            return;
        }
        foreach (var s in shields) {
            s.MiniShieldMove.MoveDirect();
        }
    }

    public bool IsShieldCompletedMove() {
        if (shields == null) {
            return false;
        }
        foreach (var s in shields) {
            if (!s.MiniShieldMove.CompleteMoveToTarget()) {
                return false;
            }
        }
        return true;
    }

    public void StartNormalRotateShield() {
        currentRotateShield = normalRotateShield;
    }

    public void StartRageRotateShield() {
        currentRotateShield = rageRotateShield;
    }

    public void RotatingShield() {
        if (shieldContainer) {
            shieldContainer.Rotate(Vector3.back, currentRotateShield * Time.deltaTime);
        }
    }
}
