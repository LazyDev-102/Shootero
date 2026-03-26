using UnityEngine;

public class MB05Attack : MinibossAttack {

    private MB05Base mb05Base;

    public MB05Base MB05Base {
        get {
            if (mb05Base == null) {
                mb05Base = MinibossBase as MB05Base;
            }
            return mb05Base;
        }
    }
}
