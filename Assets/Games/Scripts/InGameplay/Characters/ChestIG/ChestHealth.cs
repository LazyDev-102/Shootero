
using System;
using UnityEngine;

public class ChestHealth : MonoBehaviour {
    private ChestBase chestBase;
    public ChestBase ChestBase {
        get {
            if (chestBase == null) {
                chestBase = GetComponent<ChestBase>();
            }
            return chestBase;
        }
    }

    [SerializeField] protected int currentHP;

    protected Action<int, float> onHpChanged;

    public virtual int CurrentHp {
        get {
            return currentHP;
        }
        protected set {
            int maxHp = ChestBase.ChestStat.MaxHP.Value;
            currentHP = value;
            currentHP = Mathf.Clamp(currentHP, 0, maxHp);
            onHpChanged?.Invoke(currentHP, 1.0f * currentHP / maxHp);
        }
    }
    public void AddOnHpChanged(Action<int, float> onHpChanged) {
        this.onHpChanged += onHpChanged;
    }

    public void RemoveOnHpChanged(Action<int, float> onHpChanged) {
        this.onHpChanged -= onHpChanged;
    }

    public void DispatchOnHpChanged(int cur, int max) {
        onHpChanged?.Invoke(cur, 1.0f * cur / max);
    }

    public virtual void HPReduce(int hp) {
        int newHP = CurrentHp - hp;
        newHP = newHP >= 0 ? newHP : 0;
        CurrentHp = newHP;
    }

    public void AddHp(int hp) {
        CurrentHp += hp;
        if (hp > 0) {
            TextShowupManager.Instance.ShowHealingText($"+ {hp}", ChestBase.ChestMove.MyRigi.position);
        }
        else
            TextShowupManager.Instance.ShowHitText(HitType.Normal, $" {hp}", ChestBase.ChestMove.MyRigi.position);
    }
    public virtual void AddHpByPercent(float percent) {
        int hp = Mathf.CeilToInt(ChestBase.ChestStat.MaxHP.Value * percent);
        CurrentHp += hp;
        TextShowupManager.Instance.ShowHealingText($"+ {hp}", ChestBase.ChestMove.MyRigi.position);
    }

    public void ForceChangeCurrentHp(int hp) {
        CurrentHp = hp;
    }
    public virtual float GetPercentHPRemain() {
        return (float)((float)(currentHP) / (float)(ChestBase.ChestStat.MaxHP.Value));
    }
    public virtual void Initalize() {
        ForceChangeCurrentHp(ChestBase.ChestStat.MaxHP.Value);
    }

    public virtual void Destroy() {

    }
    public virtual void Updating() {
    }
}
