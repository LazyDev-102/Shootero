using UnityEngine;

public class XMB02Attack : MinibossAttack {

    private XMB02Base mb10Base;

    public XMB02Base XMB02Base {
        get {
            if (mb10Base == null) {
                mb10Base = MinibossBase as XMB02Base;
            }
            return mb10Base;
        }
    }
}
