using UnityEngine;

public class MB17Attack : MinibossAttack {

    private MB17Base mb17Base;

    public MB17Base MB17Base {
        get {
            if (mb17Base == null) {
                mb17Base = MinibossBase as MB17Base;
            }
            return mb17Base;
        }
    }

}
