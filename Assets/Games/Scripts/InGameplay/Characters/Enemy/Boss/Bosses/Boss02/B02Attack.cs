

public class B02Attack : BossAttack {
    private B02Base b02Base;

    public B02Base B02Base {
        get {
            if(b02Base == null) {
                b02Base = BossBase as B02Base;
            }
            return b02Base;
        }
    }
}
