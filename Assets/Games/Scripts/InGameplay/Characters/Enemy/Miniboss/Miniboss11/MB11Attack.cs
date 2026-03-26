using UnityEngine;

public class MB11Attack : MinibossAttack {

    private MB11Base mb11Base;

    public MB11Base MB11Base {
        get {
            if (mb11Base == null) {
                mb11Base = MinibossBase as MB11Base;
            }
            return mb11Base;
        }
    }

}
