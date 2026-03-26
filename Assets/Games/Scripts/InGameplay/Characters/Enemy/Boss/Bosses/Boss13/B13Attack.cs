
public class B13Attack : BossAttack {
    private B13Base b13Base;

    public B13Base B13Base {
        get {
            if (b13Base == null) {
                b13Base = BossBase as B13Base;
            }
            return b13Base;
        }
    }
}
