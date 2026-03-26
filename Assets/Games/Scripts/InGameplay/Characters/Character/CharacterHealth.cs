

using System;
using UnityEngine;

public abstract class CharacterHealth : MonoBehaviour {
    private CharacterBase characterBase;
    public CharacterBase CharacterBase {
        get {
            if (characterBase == null) {
                characterBase = GetComponent<CharacterBase>();
            }
            return characterBase;
        }
    }

    [SerializeField] protected int currentHP;

    protected Action<int, float> onHpChanged;

    public virtual int CurrentHp {
        get {
            return currentHP;
        }
        protected set {
            int maxHp = CharacterBase.CharacterStat.MaxHP.Value;
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

    public void AddHp(int hp, bool showEffect = true) {
        CurrentHp += hp;
        if (!showEffect)
            return;
        if (hp > 0) {
            TextShowupManager.Instance.ShowHealingText($"+ {hp}", CharacterBase.CharacterMove.MyRigi.position);
        }
        else
            TextShowupManager.Instance.ShowHitText(HitType.Normal, $" {-hp}", CharacterBase.CharacterMove.MyRigi.position);
    }
    public virtual void AddHpByPercent(float percent) {
        int hp = Mathf.CeilToInt(CharacterBase.CharacterStat.MaxHP.Value * percent);
        CurrentHp += hp;
        if (percent >= 0)
            TextShowupManager.Instance.ShowHealingText($"+ {hp}", CharacterBase.CharacterMove.MyRigi.position);
        else
            TextShowupManager.Instance.ShowHitText(HitType.Burn, $" {hp}", CharacterBase.CharacterMove.MyRigi.position);
    }
    public virtual void AddFullHP() {
        int maxHp = characterBase.CharacterStat.MaxHP.Value;
        int hp = maxHp - CurrentHp;
        CurrentHp = maxHp;
        TextShowupManager.Instance.ShowHealingText($"+ {hp}", CharacterBase.CharacterMove.MyRigi.position);
    }
    public void ForceChangeCurrentHp(int hp) {
        CurrentHp = hp;
    }
    public virtual float GetPercentHPRemain() {
        return (float)((float)(currentHP) / (float)(CharacterBase.CharacterStat.MaxHP.Value));
    }
    public virtual void Initalize() {
        ForceChangeCurrentHp(CharacterBase.CharacterStat.MaxHP.Value);
    }

    public virtual void Destroy() {

    }
    public virtual void Updating() {
    }
}
