using UnityEngine;

public class HMB01ParentAttack : MinibossAttack {

    private HMB01ParentBase mb09ParentBase;

    public HMB01ParentBase HMB01ParentBase {
        get {
            if (mb09ParentBase == null) {
                mb09ParentBase = MinibossBase as HMB01ParentBase;
            }
            return mb09ParentBase;
        }
    }
}
