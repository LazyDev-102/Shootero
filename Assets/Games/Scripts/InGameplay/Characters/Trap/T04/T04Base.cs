using UnityEngine;

public class T04Base : TrapBase {
    #region
    private T04Attack t04Attack;
    public T04Attack T04Attack {
        get {
            if (t04Attack == null) {
                t04Attack = TrapAttack as T04Attack;
            }
            return t04Attack;
        }
    }

    private T04Move t04Move;
    public T04Move T04Move {
        get {
            if (t04Move == null) {
                t04Move = TrapMove as T04Move;
            }
            return t04Move;
        }
    }

    private T04Stat t04Stat;
    public T04Stat T04Stat {
        get {
            if (t04Stat == null) {
                t04Stat = TrapStat as T04Stat;
            }
            return t04Stat;
        }
    }

    private T04Hitbox t04Hitbox;
    public T04Hitbox T04Hitbox {
        get {
            if (t04Hitbox == null) {
                t04Hitbox = TrapHitbox as T04Hitbox;
            }
            return t04Hitbox;
        }
    }
    #endregion
    [SerializeField] private RadiusHitbox radiusHitbox;
    [SerializeField] private CenterHitbox centerHitbox;
    [SerializeField] private float centerAtkPercent;


    public override void Initialize() {
        base.Initialize();
        radiusHitbox.SetDamage(T04Stat.Atk.Value);
        radiusHitbox.SetObjectBase(this);

        centerHitbox.SetDamage((int)(T04Stat.Atk.Value * centerAtkPercent));
        centerHitbox.SetObjectBase(this);
    }
}
