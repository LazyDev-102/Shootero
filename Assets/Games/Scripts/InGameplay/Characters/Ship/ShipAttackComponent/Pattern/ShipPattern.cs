using UnityEngine;

public abstract class ShipPattern : MonoBehaviour {
    [SerializeField] private Transform firePoint;
    [SerializeField] private ShipPatternCombie[] combieablePatterns;

    protected ShipAttack shipAttack;
    private FloatStat shipAtkSpeed;
    private FloatStat shipDMPercent;

    private Countdowner attackCountdowner = new Countdowner();
    private bool isAttacking;
    private bool isFocusing;
    protected GameLoader gameLoader;

    public float FireRate {
        get {
            return 1 / shipAtkSpeed.Value;
        }
    }

    protected Transform FirePoint { get => firePoint; }

    private D GetShipPatternInfo<D>(int levelIndex) where D : ShipPatternInfo {
        if (isFocusing) {
            return GetShipPatternData<D>().GetFocusPatternByLevelIndex(levelIndex);
        }
        return GetShipPatternData<D>().GetPatternByLevelIndex(levelIndex);
    }

    protected I GetCurrentShipPatternInfo<I>() where I : ShipPatternInfo {
        return GetShipPatternInfo<I>(shipAttack.CurrentLevelBulletUp);
    }

    protected abstract ShipPatternData GetShipPatternData();
    protected abstract ShipPatternData<I> GetShipPatternData<I>() where I : ShipPatternInfo;

    public virtual void Initialize() {
        isFocusing = false;
        isAttacking = false;
        ShipPatternData patternData = GetShipPatternData();
        shipAtkSpeed = shipAttack.ShipBase.ShipStat.AtkSpeed;
        shipAtkSpeed.SetBaseValue(patternData.GetAttackSpeed(shipAttack.CurrentLevelBulletUp));
        shipDMPercent = shipAttack.ShipBase.ShipStat.DMPercent;
        shipDMPercent.SetBaseValue(patternData.GetDamagePercent(shipAttack.CurrentLevelBulletUp));
        attackCountdowner.StartCountdown(FireRate);
        gameLoader = GameManager.Instance.GameLoader;
    }

    public virtual void Destroy() {
        StopAllCoroutines();
    }

    public virtual void Updating() {
        attackCountdowner.Countdowning(Time.deltaTime);
    }

    protected virtual void StartAttacking() {
        isAttacking = true;
    }

    protected abstract void DoAttacking();
    protected virtual void EndAttacking() {
        attackCountdowner.StartCountdown(FireRate);
        isAttacking = false;
    }

    public void Attack() {
        if (CanAttack()) {
            if (!GameManager.Instance.isTest) {
                SoundManager.Instance.PlayShotPlayer(shipAttack.ShipIndex);
            }
            StartAttacking();
            DoAttacking();
        }
    }

    protected virtual bool CanAttack() {
        return attackCountdowner.IsTimeOut() && !isAttacking;
    }

    public void FocusUpgrade() {
        isFocusing = true;
        ShipPatternData patternData = GetShipPatternData();
        shipAtkSpeed.SetBaseValue(patternData.GetFocusAttackSpeed(shipAttack.CurrentLevelBulletUp));
        shipDMPercent.SetBaseValue(patternData.GetFocusDamagePercent(shipAttack.CurrentLevelBulletUp));
    }

    public void BulletUp() {
        ShipPatternData patternData = GetShipPatternData();
        if (isFocusing) {
            shipAtkSpeed.SetBaseValue(patternData.GetFocusAttackSpeed(shipAttack.CurrentLevelBulletUp));
            shipDMPercent.SetBaseValue(patternData.GetFocusDamagePercent(shipAttack.CurrentLevelBulletUp));
        }
        else {
            shipAtkSpeed.SetBaseValue(patternData.GetAttackSpeed(shipAttack.CurrentLevelBulletUp));
            shipDMPercent.SetBaseValue(patternData.GetDamagePercent(shipAttack.CurrentLevelBulletUp));
        }
    }

    public void SetShipAttack(ShipAttack shipAttack) {
        this.shipAttack = shipAttack;
    }

    public abstract void SetShipAttackComponent<T>(T shipAttackComponent) where T : ShipAttackComponent;

    public ShipPatternCombie[] GetCombiePatterns() {
        return combieablePatterns;
    }
}
public abstract class ShipPattern<T> : ShipPattern where T : ShipAttackComponent {

    protected T shipAttackComponent;

    public override void SetShipAttackComponent<T1>(T1 shipAttackComponent) {
        this.shipAttackComponent = shipAttackComponent as T;
    }
}


[System.Serializable]
public class ShipPatternCombie {
    [SerializeField] private PatternModData modData;
    [SerializeField] private ShipPattern shipPattern;

    public PatternModData ModData { get => modData; }
    public ShipPattern ShipPattern { get => shipPattern; }
}