using UnityEngine;

public class MB15ParentAttack : MinibossAttack {

    private MB15ParentBase mb15ParentBase;

    public MB15ParentBase MB15ParentBase {
        get {
            if (mb15ParentBase == null) {
                mb15ParentBase = MinibossBase as MB15ParentBase;
            }
            return mb15ParentBase;
        }
    }
}
