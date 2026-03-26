using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DronePattern : MonoBehaviour
{
   [SerializeField] private Transform firePoint;
    protected DroneAttack droneAttack;
    private FloatStat droneAtkSpeed;
    private FloatStat droneDMPercent;

    private Countdowner attackCountdowner = new Countdowner();
    private bool isAttacking;
    private bool isFocusing;
    protected GameLoader gameLoader;

    public float FireRate {
        get {
            return 1 / droneAtkSpeed.Value;
        }
    }

    protected Transform FirePoint { get => firePoint; }

    private D GetDronePatternInfo<D>(int levelIndex) where D : DronePatternInfor {
        if(isFocusing) {
            return GetDronePatternData<D>().GetFocusPatternByLevelIndex(levelIndex);
        }
        return GetDronePatternData<D>().GetPatternByLevelIndex(levelIndex);
    }

    protected I GetCurrentDronePatternInfo<I>() where I : DronePatternInfor {
        return GetDronePatternInfo<I>(0);
    }
    protected abstract DronePatternData GetDronePatternData();
    protected abstract DronePatternData<I> GetDronePatternData<I>() where I : DronePatternInfor;

    public virtual void Initialize() {
        DronePatternData patternData = GetDronePatternData();
        droneAtkSpeed.SetBaseValue(patternData.AttackSpeed);
        droneDMPercent.SetBaseValue(patternData.DamgePercent);
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


    public void SetDroneAttack(DroneAttack droneAttack) {
        this.droneAttack = droneAttack;
    }

    public abstract void SetDroneAttackComponent<T>(T droneAttackComponent) where T : DroneAttackComponent;

    //public ShipPatternCombie[] GetCombiePatterns() {
    //    return combieablePatterns;
    //}

}
public abstract class DronePattern<T> : DronePattern where T : DroneAttackComponent {

    protected T dronettackComponent;

    public override void SetDroneAttackComponent<T1>(T1 droneAttackComponent) {
        this.dronettackComponent = droneAttackComponent as T;
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
