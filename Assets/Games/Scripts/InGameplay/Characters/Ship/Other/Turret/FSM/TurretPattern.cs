using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretPattern : MonoBehaviour
{
   [SerializeField] private Transform firePoint;
    protected TurretAttack TurretAttack;
    private FloatStat TurretAtkSpeed;
    private FloatStat TurretDMPercent;

    private Countdowner attackCountdowner = new Countdowner();
    private bool isAttacking;
    private bool isFocusing;
    protected GameLoader gameLoader;

    public float FireRate {
        get {
            return 1 / TurretAtkSpeed.Value;
        }
    }

    protected Transform FirePoint { get => firePoint; }

    private D GetTurretPatternInfo<D>(int levelIndex) where D : TurretPatternInfor {
        if(isFocusing) {
            return GetTurretPatternData<D>().GetFocusPatternByLevelIndex(levelIndex);
        }
        return GetTurretPatternData<D>().GetPatternByLevelIndex(levelIndex);
    }

    protected I GetCurrentTurretPatternInfo<I>() where I : TurretPatternInfor {
        return GetTurretPatternInfo<I>(0);
    }
    protected abstract TurretPatternData GetTurretPatternData();
    protected abstract TurretPatternData<I> GetTurretPatternData<I>() where I : TurretPatternInfor;

    public virtual void Initialize() {
        TurretPatternData patternData = GetTurretPatternData();
        TurretAtkSpeed.SetBaseValue(patternData.AttackSpeed);
        TurretDMPercent.SetBaseValue(patternData.DamgePercent);
        attackCountdowner.StartCountdown(FireRate);
        isAttacking = false;
        isFocusing = false;
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
        if(CanAttack()) {
            StartAttacking();
            DoAttacking();
        }
    }

    protected virtual bool CanAttack() {
        return attackCountdowner.IsTimeOut() && !isAttacking;
    }

    public void FocusUpgrade() {
        isFocusing = true;
    }


    public void SetTurretAttack(TurretAttack TurretAttack) {
        this.TurretAttack = TurretAttack;
    }

    public abstract void SetTurretAttackComponent<T>(T TurretAttackComponent) where T : TurretAttackComponent;

    //public ShipPatternCombie[] GetCombiePatterns() {
    //    return combieablePatterns;
    //}

}
public abstract class TurretPattern<T> : TurretPattern where T : TurretAttackComponent {

    protected T TurretttackComponent;

    public override void SetTurretAttackComponent<T1>(T1 TurretAttackComponent) {
        this.TurretttackComponent = TurretAttackComponent as T;
    }
    // [SerializeField] private ShipPatternCombie[] combieablePatterns;
}


//[System.Serializable]
//public class ShipPatternCombie {
//    [SerializeField] private int idMod;
//    [SerializeField] private ShipPattern shipPattern;

//    public int IdMod { get => idMod; }
//    public ShipPattern ShipPattern { get => shipPattern; }
//}
