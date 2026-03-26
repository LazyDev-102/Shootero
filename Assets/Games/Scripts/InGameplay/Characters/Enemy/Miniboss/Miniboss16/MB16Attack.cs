using UnityEngine;

public class MB16Attack : MinibossAttack {

    private MB16Base mb16Base;

    public MB16Base MB16Base {
        get {
            if (mb16Base == null) {
                mb16Base = MinibossBase as MB16Base;
            }
            return mb16Base;
        }
    }

}
