using UnityEngine;

public class MB09ParentAttack : MinibossAttack {

    private MB09ParentBase mb09ParentBase;

    public MB09ParentBase MB09ParentBase {
        get {
            if (mb09ParentBase == null) {
                mb09ParentBase = MinibossBase as MB09ParentBase;
            }
            return mb09ParentBase;
        }
    }
}
