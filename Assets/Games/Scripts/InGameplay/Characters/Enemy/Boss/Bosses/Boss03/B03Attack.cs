using UnityEngine;

public class B03Attack : BossAttack {
    private B03Base b03Base;

    public B03Base B03Base {
        get {
            if(b03Base == null) {
                b03Base = BossBase as B03Base;
            }
            return b03Base;
        }
    }

}
