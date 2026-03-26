using UnityEngine;

public class MB03Attack : MinibossAttack {

    private MB03Base mb03Base;

    public MB03Base MB03Base {
        get {
            if (mb03Base == null) {
                mb03Base = MinibossBase as MB03Base;
            }
            return mb03Base;
        }
    }
}
