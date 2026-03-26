using UnityEngine;

public class HMB02Attack : MinibossAttack {

    private HMB02Base mb10Base;

    public HMB02Base HMB02Base {
        get {
            if (mb10Base == null) {
                mb10Base = MinibossBase as HMB02Base;
            }
            return mb10Base;
        }
    }
}
