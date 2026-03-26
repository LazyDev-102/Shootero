using UnityEngine;

public class XMB01Attack : MinibossAttack {

    private XMB01Base mb09Base;

    public XMB01Base XMB01Base {
        get {
            if (mb09Base == null) {
                mb09Base = MinibossBase as XMB01Base;
            }
            return mb09Base;
        }
    }
}
