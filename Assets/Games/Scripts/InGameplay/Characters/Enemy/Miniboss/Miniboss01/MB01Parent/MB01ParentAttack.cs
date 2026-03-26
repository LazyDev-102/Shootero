using UnityEngine;

public class MB01ParentAttack : MinibossAttack {

    private MB01ParentBase mb01ParentBase;

    public MB01ParentBase MB01ParentBase {
        get {
            if (mb01ParentBase == null) {
                mb01ParentBase = MinibossBase as MB01ParentBase;
            }
            return mb01ParentBase;
        }
    }
}
