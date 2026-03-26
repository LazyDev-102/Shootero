

public class XB01Attack : BossAttack {
    private XB01Base xb01Base;

    public XB01Base XB01Base {
        get {
            if (xb01Base == null) {
                xb01Base = BossBase as XB01Base;
            }
            return xb01Base;
        }
    }
}
