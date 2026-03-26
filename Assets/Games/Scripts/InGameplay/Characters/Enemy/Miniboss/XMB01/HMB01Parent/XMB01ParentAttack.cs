using UnityEngine;

public class XMB01ParentAttack : MinibossAttack {

    private XMB01ParentBase mb09ParentBase;

    public XMB01ParentBase XMB01ParentBase {
        get {
            if (mb09ParentBase == null) {
                mb09ParentBase = MinibossBase as XMB01ParentBase;
            }
            return mb09ParentBase;
        }
    }
}
