

public class B10Attack : BossAttack {
    private B10Base b10Base;

    public B10Base B10Base {
        get {
            if (b10Base == null) {
                b10Base = BossBase as B10Base;
            }
            return b10Base;
        }
    }
}
