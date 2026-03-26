using UnityEngine;

public class MB01Attack : MinibossAttack {

    private MB01Base mb01Base;

    public MB01Base MB01Base {
        get {
            if (mb01Base == null) {
                mb01Base = MinibossBase as MB01Base;
            }
            return mb01Base;
        }
    }
}
