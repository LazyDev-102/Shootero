using UnityEngine;

public abstract class ShipAttackComponent : MonoBehaviour {
    private ShipPattern currentPattern;
    private ShipAttack shipAttack;
    private bool isFocusing;

    protected abstract ShipPattern GetBasicPattern();
    public ShipPattern CurrentPattern { get => currentPattern; }

    public virtual void Initialize() {
        isFocusing = false;
        ChangeToPattern(GetBasicPattern());

    }

    public abstract void PreloadIngame();

    public virtual void Updating() {
        if (currentPattern) {
            currentPattern.Updating();
        }
    }

    public void ChangeToPattern(ShipPattern newPattern) {
        if (currentPattern) {
            currentPattern.Destroy();
#if UNITY_EDITOR
            Object.DestroyImmediate(currentPattern.gameObject);
#else
            Object.Destroy(currentPattern.gameObject);
#endif
        }
        currentPattern = Instantiate(newPattern, transform);
        currentPattern.SetShipAttackComponent(this);
        currentPattern.SetShipAttack(shipAttack);
        currentPattern.Initialize();
        if (isFocusing) {
            currentPattern.FocusUpgrade();
        }
    }

    public void SetShipAttack(ShipAttack shipAttack) {
        this.shipAttack = shipAttack;
    }

    public void Attack() {
        if (currentPattern) {
            currentPattern.Attack();
        }
    }

    public void FocusUpgrade() {
        isFocusing = true;
        if (currentPattern) {
            currentPattern.FocusUpgrade();
        }
    }

    public void BulletUp() {
        if (currentPattern) {
            currentPattern.BulletUp();
        }
    }
}

