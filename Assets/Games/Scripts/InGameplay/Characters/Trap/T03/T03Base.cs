using UnityEngine;

public class T03Base : TrapBase {
    #region
    private T03Attack t03Attack;
    public T03Attack T03Attack {
        get {
            if (t03Attack == null) {
                t03Attack = TrapAttack as T03Attack;
            }
            return t03Attack;
        }
    }

    private T03Move t03Move;
    public T03Move T03Move {
        get {
            if (t03Move == null) {
                t03Move = TrapMove as T03Move;
            }
            return t03Move;
        }
    }

    private T03Stat t03Stat;
    public T03Stat T03Stat {
        get {
            if (t03Stat == null) {
                t03Stat = TrapStat as T03Stat;
            }
            return t03Stat;
        }
    }

    private T03Hitbox t03Hitbox;
    public T03Hitbox T03Hitbox {
        get {
            if (t03Hitbox == null) {
                t03Hitbox = TrapHitbox as T03Hitbox;
            }
            return t03Hitbox;
        }
    }
    #endregion
    [SerializeField] private RadiusHitbox radiusHitbox;
    [SerializeField] private CenterHitbox centerHitbox;
    [SerializeField] private float centerAtkPercent;


    public override void Initialize() {
        base.Initialize();
        radiusHitbox.SetDamage(T03Stat.Atk.Value);
        radiusHitbox.SetObjectBase(this);

        centerHitbox.SetDamage((int)(T03Stat.Atk.Value * centerAtkPercent));
        centerHitbox.SetObjectBase(this);
    }
}
