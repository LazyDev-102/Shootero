

public class B01Attack : BossAttack {
    private B01Base b01Base;

    public B01Base B01Base {
        get {
            if(b01Base == null) {
                b01Base = BossBase as B01Base;
            }
            return b01Base;
        }
    }
}
