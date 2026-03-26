using UnityEngine;

public class HMB01Attack : MinibossAttack {

    private HMB01Base mb09Base;

    public HMB01Base HMB01Base {
        get {
            if (mb09Base == null) {
                mb09Base = MinibossBase as HMB01Base;
            }
            return mb09Base;
        }
    }
}
