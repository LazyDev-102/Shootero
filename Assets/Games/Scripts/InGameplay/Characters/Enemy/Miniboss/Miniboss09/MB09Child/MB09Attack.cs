using UnityEngine;

public class MB09Attack : MinibossAttack {

    private MB09Base mb09Base;

    public MB09Base MB09Base {
        get {
            if (mb09Base == null) {
                mb09Base = MinibossBase as MB09Base;
            }
            return mb09Base;
        }
    }
}
