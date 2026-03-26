using UnityEngine;

public class MB10Attack : MinibossAttack {

    private MB10Base mb10Base;

    public MB10Base MB10Base {
        get {
            if (mb10Base == null) {
                mb10Base = MinibossBase as MB10Base;
            }
            return mb10Base;
        }
    }
}
