using UnityEngine;

public class MB13Attack : MinibossAttack {

    private MB13Base mb13Base;

    public MB13Base MB13Base {
        get {
            if (mb13Base == null) {
                mb13Base = MinibossBase as MB13Base;
            }
            return mb13Base;
        }
    }

}
