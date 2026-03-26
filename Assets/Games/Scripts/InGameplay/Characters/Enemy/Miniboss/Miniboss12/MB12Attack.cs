using UnityEngine;

public class MB12Attack : MinibossAttack {

    private MB12Base mb12Base;

    public MB12Base MB12Base {
        get {
            if (mb12Base == null) {
                mb12Base = MinibossBase as MB12Base;
            }
            return mb12Base;
        }
    }
}
